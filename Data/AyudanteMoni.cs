using System.Globalization;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MonitorVentas.Models;

namespace MonitorVentas.Data;

/// <summary>
/// Respuesta de Moni.
/// Texto = respuesta sencilla · Detalle = explicación completa («Más información») ·
/// Guia = guía paso a paso dentro de la ventana (wwwroot/js/moni-guias.js) · Modulo = ventana que se puede abrir.
/// </summary>
public record RespuestaMoni(string Texto, string? Modulo = null, string[]? Sugerencias = null,
                            string? Detalle = null, string? Guia = null, bool PideGuia = false);

/// <summary>
/// "Cerebro" de Moni, el ayudante del escritorio.
/// 1) Primero revisa si la pregunta pide un DATO EN VIVO (ventas de hoy, stock bajo...) y lo consulta en la base de datos.
/// 2) Si no, busca en su BASE DE CONOCIMIENTO el tema con más palabras clave en común.
/// Cada tema tiene una respuesta corta, una explicación completa y, si aplica, una guía paso a paso.
/// Todo se compara sin tildes ni mayúsculas, así "¿Cómo anulo una venta?" y "como anular venta" dan lo mismo.
/// </summary>
public static class AyudanteMoni
{
    private record Tema(string[] Claves, string Respuesta, string? Modulo = null, string[]? Relacionadas = null,
                        string? Detalle = null, string? Guia = null);

    public static readonly string[] SugerenciasIniciales =
    {
        "¿Cuánto se vendió hoy?", "¿Qué productos se están agotando?", "¿Cómo hago una venta?", "¿Cómo funciona la IA?"
    };

    // Frases que indican que la persona quiere aprender o que la acompañen
    private static readonly string[] PideAyuda =
    {
        "paso a paso", "guiame", "guia", "ensename", "muestrame", "explicame", "no entiendo", "no se como",
        "como funciona", "como se usa", "como uso", "como utilizo", "que hago", "ayudame", "tutorial"
    };

    // ------------------------------------------------------------------
    // Base de conocimiento (ayuda del sistema)
    // ------------------------------------------------------------------
    private static readonly Tema[] Temas =
    {
        new(new[] { "que es monitor", "que es esto", "que hace el sistema", "para que sirve monitor", "para que sirve el sistema", "de que trata" },
            "Monitor es un sistema de ventas para supermercado: lleva clientes, productos, ventas, compras, devoluciones e inventario.",
            null, new[] { "¿Cómo hago una venta?", "¿Qué tecnologías usa?" },
            "Está organizado como un escritorio: cada ícono abre un módulo en su propia ventana. " +
            "Los cinco primeros son catálogos sencillos (clientes, categorías, proveedores, empleados y métodos de pago). " +
            "Luego vienen productos, ajustes de inventario y promociones, que combinan varias tablas. " +
            "Los más completos son Ventas, Compras y Devoluciones: tienen encabezado y varios renglones, y mueven el stock solos. " +
            "Además hay un dashboard en vivo (el Monitor de ventas), 10 reportes y un Asistente IA que predice cuándo se agota cada producto. " +
            "Si quiere, le doy el recorrido del escritorio: pulse «Repetir tutorial»."),

        // ---------- Catálogos básicos ----------
        new(new[] { "cliente", "registrar cliente", "nuevo cliente", "agregar cliente" },
            "En «Clientes» se registran las personas que compran: nombre, teléfono y dirección.",
            "clientes", new[] { "¿Cómo hago una venta?" },
            "Cómo funciona: la ventana muestra la lista de clientes con un buscador que filtra mientras escribe, sin importar tildes. " +
            "Con «Nuevo cliente» se abre el formulario: el nombre y el teléfono son obligatorios, y el teléfono no se puede repetir. " +
            "Después, al vender, el cliente se elige en la caja (si no se elige, la venta queda como «Consumidor final»). " +
            "Un cliente que ya tiene compras no se puede eliminar, para no perder el historial. " +
            "¿Hacemos uno de ejemplo juntos? Pulse «Guíeme paso a paso».",
            "clientes"),

        new(new[] { "categoria", "agrupar productos" },
            "Las categorías agrupan los productos: Abarrotes, Bebidas, Lácteos, Limpieza...",
            "categorias", null,
            "Cada producto pertenece a una categoría. Sirven para filtrar el catálogo, para el reporte «Ventas por categoría» " +
            "y para crear promociones que aplican a toda una categoría de una vez. El nombre no se puede repetir, " +
            "y una categoría que ya tiene productos no se puede eliminar.",
            "categorias"),

        new(new[] { "proveedor", "distribuidora" },
            "Los proveedores son las empresas que surten al supermercado.",
            "proveedores", new[] { "¿Cómo registro una compra?" },
            "Cada producto tiene un proveedor, y las compras se hacen por proveedor: al registrar una factura solo aparecen sus productos. " +
            "Se guarda el nombre de la empresa, el contacto, el teléfono, el correo y la dirección. " +
            "Un proveedor con productos o compras no se puede eliminar.",
            "proveedores"),

        new(new[] { "empleado", "cajero", "nuevo empleado", "dui", "inactivar", "despedir" },
            "En «Empleados» se registra al personal: nombre, DUI, cargo y teléfono.",
            "empleados", null,
            "El DUI se escribe con el formato 00000000-0 (el guion se pone solo) y no se puede repetir. " +
            "Los empleados activos aparecen como cajeros en la caja y como responsables en compras, ajustes y devoluciones. " +
            "Si un empleado ya tiene movimientos, no se elimina: se edita y se desmarca «Activo». " +
            "Así deja de aparecer en las listas, pero su historial queda intacto.",
            "empleados"),

        new(new[] { "metodo de pago", "efectivo", "tarjeta", "transferencia", "forma de pago" },
            "Los métodos de pago son las formas de cobrar: Efectivo, Tarjeta, Transferencia...",
            "metodos", null,
            "Se eligen en la caja al cobrar y aparecen en el reporte «Ventas por método de pago». " +
            "Si un método ya se usó en ventas no se puede borrar; se desmarca «Disponible» para que deje de salir en la caja.",
            "metodos"),

        // ---------- Intermedios ----------
        new(new[] { "producto", "agregar producto", "nuevo producto", "crear producto", "registrar producto", "catalogo" },
            "En «Productos» está el catálogo del supermercado, con precio y existencias (stock).",
            "productos", new[] { "¿Por qué no puedo eliminar un producto?", "¿Cómo registro una compra?" },
            "Cada producto tiene categoría, proveedor, precio de venta y stock. El código de barras es opcional, pero debe tener 13 dígitos. " +
            "El stock no se edita a mano después: se mueve solo con las ventas (baja), las compras y devoluciones reintegradas (sube) " +
            "y los ajustes de inventario. Las filas en rojo tienen 5 unidades o menos. " +
            "¿Registramos uno juntos? Pulse «Guíeme paso a paso».",
            "productos"),

        new(new[] { "codigo de barras", "ean", "lector", "escaner", "13 digitos" },
            "El código de barras usa el formato EAN-13: exactamente 13 dígitos.",
            "productos", null,
            "Es opcional al registrar un producto. En la caja se puede escribir o pasar con un lector: al pulsar Enter, " +
            "el producto se agrega solo a la venta. No se puede repetir entre productos.",
            "productos"),

        new(new[] { "ajuste", "ajuste de inventario", "merma", "vencido", "danado", "robo", "conteo fisico", "corregir stock" },
            "Los ajustes de inventario registran entradas o salidas que no son ventas ni compras.",
            "ajustes", null,
            "Ejemplos: productos vencidos o dañados (salida), un robo (salida) o un conteo físico que encontró más unidades (entrada). " +
            "Cada ajuste mueve el stock al guardarse y queda registrado con su responsable y motivo. " +
            "Una salida nunca puede dejar el stock en negativo, y si se elimina un ajuste, su efecto se revierte.",
            "ajustes"),

        new(new[] { "promocion", "descuento", "oferta", "rebaja" },
            "En «Promociones» se crean descuentos para un producto o una categoría, con fechas.",
            "promociones", null,
            "Se elige el alcance (un producto o toda una categoría), el porcentaje (1 % a 90 %) y las fechas de inicio y fin. " +
            "Mientras está vigente, la caja aplica el descuento sola: muestra el precio tachado y el ticket lleva la etiqueta PROMO. " +
            "Si un producto tiene varias promociones a la vez, se aplica la mejor. El precio con descuento queda guardado en la venta.",
            "promociones"),

        // ---------- Avanzados ----------
        new(new[] { "hacer una venta", "venta", "vender", "nueva venta", "registrar venta", "cobrar", "punto de venta", "caja" },
            "Las ventas se hacen en «Nueva venta»: se agregan los productos y se cobra.",
            "ventas", new[] { "¿Cómo anulo una venta?", "¿Cómo funcionan las promociones?" },
            "Primero se elige el cajero, el método de pago y, si quiere, el cliente. Luego se busca cada producto por nombre o código de barras, " +
            "se pone la cantidad y se pulsa Agregar (o Enter). El sistema no deja vender más de lo que hay en stock y aplica las promociones vigentes. " +
            "Al pulsar «Cobrar», todo se guarda de una vez: el stock baja, el precio queda guardado tal como se cobró y sale el ticket. " +
            "Atajo: F2 vuelve al buscador de productos.",
            "ventas"),

        new(new[] { "anular venta", "anular", "cancelar venta", "borrar venta", "eliminar venta", "venta equivocada" },
            "Las ventas no se borran: se anulan desde el «Historial de ventas».",
            "historial", new[] { "¿Cómo hago una devolución?" },
            "Al anular, la venta queda en el historial marcada como anulada (para que nada desaparezca) y el stock de sus productos se devuelve solo. " +
            "Una venta anulada ya no cuenta en los reportes ni en el dashboard. " +
            "Si la venta ya tiene devoluciones, no se puede anular: para eso están las devoluciones parciales.",
            "anular"),

        new(new[] { "devolucion", "devolver", "devolver producto", "regresar producto", "reembolso", "cliente devuelve" },
            "Una devolución registra los productos que el cliente regresa de una venta.",
            "devoluciones", new[] { "¿Qué es el precio histórico?" },
            "Se hace desde el ticket de la venta: se indica cuántas unidades regresan de cada producto (sin pasar de lo vendido), " +
            "el motivo y quién atiende. Si se marca «Reintegrar al inventario», el stock sube; si el producto está vencido o dañado, se descarta. " +
            "El dinero se calcula con el precio que pagó el cliente ese día, aunque el precio haya cambiado después.",
            "devoluciones"),

        new(new[] { "compra", "proveedor entrega", "factura", "factura proveedor", "recibir mercaderia", "reabastecer", "pedido" },
            "En «Compras» se registra la factura del proveedor; al guardarla, el stock sube.",
            "compras", new[] { "¿Qué es la compra sugerida?" },
            "Se elige el proveedor (solo aparecen sus productos), el número de factura (no se repite para el mismo proveedor) y quién recibe. " +
            "Al elegir un producto se sugiere el último costo pagado, y si el costo supera el precio de venta, el sistema avisa. " +
            "Una compra se puede anular mientras haya stock suficiente para quitar lo que entró.",
            "compras"),

        // ---------- Reportes e IA ----------
        new(new[] { "reporte", "excel", "imprimir", "exportar", "informe" },
            "Hay 10 reportes, con filtro de fechas, gráfico, impresión y exportación a Excel.",
            "reportes", new[] { "¿Qué es el kardex?" },
            "Los reportes son: ventas por día, productos más vendidos, ventas por categoría, por cajero, por método de pago, mejores clientes, " +
            "inventario valorizado, kardex de producto, compras por proveedor y devoluciones por motivo. " +
            "Todos muestran un resumen, un gráfico de barras y una tabla con totales. Las ventas y compras anuladas nunca cuentan.",
            "reportes"),

        new(new[] { "kardex", "movimientos de un producto", "historial de producto", "entradas y salidas" },
            "El kardex muestra todas las entradas y salidas de un producto, con su saldo.",
            "reportes", null,
            "Está en Reportes → 08. Se elige el producto y las fechas, y aparece cada movimiento en orden: ventas, compras, devoluciones reintegradas " +
            "y ajustes, con el saldo después de cada uno. El último saldo siempre coincide con el stock actual, así se comprueba que el inventario cuadra.",
            "reportes"),

        new(new[] { "ia", "inteligencia artificial", "asistente ia", "prediccion", "ml net", "machine learning", "pronostico" },
            "El Asistente IA predice cuánto se venderá cada producto y cuándo se agota.",
            "asistente", new[] { "¿Qué es la compra sugerida?", "¿Qué productos se están agotando?" },
            "Usa ML.NET, la librería de inteligencia artificial de Microsoft. Para cada producto arma la lista de unidades vendidas por día " +
            "(últimos 90 días) y entrena un modelo SSA que aprende la tendencia y el ciclo semanal (los fines de semana se vende más). " +
            "Pronostica los próximos 7 días con un rango probable, calcula en cuántos días se agota con el stock actual y sugiere cuánto comprar " +
            "para 14 días. Si un producto tiene menos de 28 días de historial, usa un promedio simple.",
            "asistente"),

        new(new[] { "compra sugerida", "sugerencia de compra", "cuanto comprar" },
            "La compra sugerida es la compra que arma el Asistente IA con lo que conviene pedir.",
            "asistente", null,
            "En el Asistente IA, cada proveedor con productos por agotarse tiene el botón «Crear compra sugerida». " +
            "Abre Compras con el proveedor, los productos, las cantidades y los últimos costos ya llenos. Solo falta revisar y escribir la factura.",
            "asistente"),

        new(new[] { "datos de demostracion", "datos de prueba", "demo", "historial falso", "generar datos" },
            "«Datos de demostración» crea 90 días de ventas y compras de ejemplo.",
            "asistente", null,
            "Sirve para que la IA y los reportes tengan de dónde aprender en una presentación. Crea ventas diarias con más movimiento los fines de semana " +
            "y compras semanales que reponen exactamente lo vendido, así el stock actual no cambia. Solo se puede hacer una vez."),

        new(new[] { "dashboard", "monitor de ventas", "panel", "indicadores", "pantalla verde" },
            "El Monitor de ventas (arriba a la derecha) es el dashboard en vivo.",
            null, null,
            "Muestra las ventas y el monto de hoy, cuántos clientes hay y cuántos productos tienen stock bajo. " +
            "Se actualiza solo cada 30 segundos y avisa con un globo cuando entran ventas nuevas o hay productos por agotarse. " +
            "Con el botón ↻ se actualiza al momento."),

        // ---------- Reglas y uso general ----------
        new(new[] { "precio historico", "cambio de precio", "subir precio", "precio viejo" },
            "Cada venta guarda el precio con el que se cobró, aunque después cambie.",
            null, null,
            "Si mañana sube el precio de un producto, las ventas anteriores no cambian: cada renglón guarda su precio unitario y el descuento aplicado. " +
            "Por eso los reportes de meses pasados siguen siendo correctos y las devoluciones devuelven exactamente lo que pagó el cliente."),

        new(new[] { "no puedo eliminar", "no deja eliminar", "no se puede eliminar", "por que no puedo borrar", "eliminar" },
            "Monitor no deja eliminar lo que ya se usó, para proteger el historial.",
            null, null,
            "Por ejemplo: un producto vendido, un empleado con ventas o un método de pago usado. En esos casos se desactiva " +
            "(«Activo» o «Disponible») o, si es una venta o compra, se anula. La pantalla de eliminar explica el motivo y quita el botón."),

        new(new[] { "buscar", "busqueda", "filtrar", "encontrar" },
            "Cada lista tiene un buscador que filtra mientras escribe.",
            null, null,
            "No importan las tildes ni las mayúsculas: «gomez» encuentra «Gómez». También busca en el servidor al pulsar «Buscar». Atajo: F3 lleva al buscador."),

        new(new[] { "atajo", "atajos", "teclado", "tecla", "f2", "f3" },
            "Atajos: F3 buscador · Alt+N nuevo registro · F2 buscador de la caja · Enter agrega el producto.",
            null, null,
            "En la caja y en Compras, Enter agrega el producto (sirve con lector de código de barras). En la intro, Enter la salta. " +
            "En los diálogos, Enter acepta y Escape cierra."),

        new(new[] { "validacion", "validaciones", "error en el formulario", "campo obligatorio", "datos invalidos", "asterisco" },
            "Los campos con asterisco rojo son obligatorios; si algo está mal, se marca en rojo con el motivo.",
            null, null,
            "Monitor valida en tres capas: en el navegador mientras escribe, en el servidor (reglas como stock suficiente o nombres repetidos) " +
            "y en la base de datos (restricciones). Así, aunque alguien salte una capa, los datos nunca quedan inválidos."),

        new(new[] { "fondo", "fondo de escritorio", "cambiar color", "imagen de fondo", "personalizar" },
            "Clic derecho en el escritorio → «Fondo de escritorio...».",
            null, null,
            "Puede elegir un color o subir una imagen propia, y decidir si se ajusta, se estira o se repite. Se guarda en este navegador."),

        new(new[] { "tecnologia", "tecnologias", "lenguaje", "base de datos", "sql server", "asp net", "entity framework", "con que esta hecho" },
            "Monitor está hecho en C# con ASP.NET Core MVC (.NET 10), Entity Framework Core y SQL Server.",
            null, null,
            "La base de datos se crea con migraciones de Entity Framework (Code First). La IA usa ML.NET (modelo SSA de series de tiempo). " +
            "La interfaz es HTML, CSS y JavaScript con un diseño propio inspirado en los escritorios de los 90, y Excel se genera sin librerías externas."),

        new(new[] { "formularios", "criterios", "bitacora", "crud", "basicos", "intermedios", "avanzados" },
            "5 formularios básicos, 3 intermedios y 3 avanzados, más dashboard, 10 reportes e IA.",
            null, null,
            "Básicos (una tabla): Clientes, Categorías, Proveedores, Empleados y Métodos de pago. " +
            "Intermedios (varias tablas): Productos, Ajustes de inventario y Promociones. " +
            "Avanzados (encabezado y detalle, con transacciones): Ventas, Compras y Devoluciones. " +
            "Todos tienen búsqueda interactiva y validación."),

        new(new[] { "restablecer", "borrar todo", "empezar de cero", "limpiar datos", "dejar vacio", "reiniciar sistema" },
            "En Inicio → «Restablecer sistema...» se borran todos los datos.",
            null, null,
            "Monitor queda como recién instalado, solo con las categorías y métodos de pago iniciales, y los números vuelven a empezar en 1. " +
            "Hay que escribir RESTABLECER para confirmar, y no se puede deshacer."),

        new(new[] { "salir", "apagar", "cerrar monitor", "cerrar el sistema", "cerrar sesion", "terminar" },
            "Para salir use Inicio → «Apagar...» (o Ctrl+Q).",
            null, null,
            "Aparece un diálogo para Apagar o Reiniciar. Al apagar me despido, se cierran las ventanas y la pantalla se apaga como un monitor de tubo. " +
            "Por seguridad, una página web no puede cerrar la pestaña del navegador: al final queda una pantalla negra donde ya puede cerrarla, " +
            "o pulsar «Encender» para volver a empezar."),

        new(new[] { "tutorial", "recorrido", "explicame el escritorio" },
            "¡Claro! Pulse «Repetir tutorial» y le muestro el escritorio ícono por ícono.",
            null, null),

        new(new[] { "quien eres", "como te llamas", "tu nombre", "que eres" },
            "Soy Moni, el ayudante de Monitor.",
            null, SugerenciasIniciales,
            "Puedo explicarle cualquier módulo de forma sencilla o a fondo, acompañarlo paso a paso dentro de las ventanas " +
            "y darle datos al momento, como las ventas de hoy o qué se está agotando."),

        new(new[] { "hola", "buenas", "buenos dias", "buenas tardes", "buenas noches", "hey" },
            "¡Hola! Soy Moni. Pregúnteme cómo funciona cualquier parte del sistema o pídame un dato.",
            null, SugerenciasIniciales),

        new(new[] { "gracias", "muchas gracias", "excelente", "genial", "perfecto" },
            "¡Con gusto! Aquí estaré si necesita algo más.",
            null),
    };

    // ------------------------------------------------------------------
    // Responder
    // ------------------------------------------------------------------
    public static async Task<RespuestaMoni> ResponderAsync(AppDbContext db, string? pregunta)
    {
        var t = Normalizar(pregunta);
        if (t.Length == 0)
            return new("Escríbame una pregunta. Por ejemplo:", null, SugerenciasIniciales);

        // 1) Datos en vivo
        var dato = await DatoEnVivoAsync(db, t);
        if (dato != null) return dato;

        // 2) Base de conocimiento: gana el tema con más coincidencias (las frases largas pesan más)
        var palabras = t.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToHashSet();
        var mejor = Temas
            .Select(tema => (tema, puntos: tema.Claves.Sum(c => Coincide(palabras, c) ? Contenido(c).Length * 2 + 1 : 0)))
            .Where(x => x.puntos > 0)
            .OrderByDescending(x => x.puntos)
            .FirstOrDefault();

        if (mejor.tema != null)
        {
            var tema = mejor.tema;
            // Si pide que le expliquen o lo guíen, se ofrece primero la guía (o se responde directo con el detalle)
            var pide = Tiene(t, PideAyuda);
            var texto = pide && tema.Guia == null && tema.Detalle != null ? tema.Detalle : tema.Respuesta;
            return new(texto, tema.Modulo, tema.Relacionadas,
                       texto == tema.Detalle ? null : tema.Detalle, tema.Guia, pide && tema.Guia != null);
        }

        return new("Mmm, eso no lo sé todavía. Pruebe preguntarlo con otras palabras o elija una de estas:",
                   null, new[] { "¿Cómo hago una venta?", "¿Cómo anulo una venta?", "¿Qué es el kardex?", "¿Cuánto se vendió hoy?" });
    }

    // Palabras que no aportan significado ("como", "una", "el"...)
    private static readonly HashSet<string> Vacias = new()
    {
        "a", "al", "de", "del", "el", "la", "las", "los", "un", "una", "unos", "unas", "y", "o", "en", "con", "por", "para",
        "que", "como", "se", "me", "mi", "es", "lo", "le", "su", "hay", "puedo", "quiero"
    };

    // Raíz sencilla: "anular" → "anul", así también coincide con "anulo", "anulada"...
    private static string Raiz(string palabra) => palabra.Length >= 5 ? palabra[..^2] : palabra;

    private static string[] Contenido(string clave) => clave.Split(' ').Where(p => !Vacias.Contains(p)).ToArray();

    // Una clave coincide si TODAS sus palabras importantes aparecen en la pregunta (en cualquier orden y forma)
    private static bool Coincide(HashSet<string> palabras, string clave)
    {
        var partes = Contenido(clave);
        return partes.Length > 0 && partes.All(p => palabras.Any(w => w.StartsWith(Raiz(p))));
    }

    private static bool Tiene(string t, params string[] frases) => frases.Any(f => t.Contains(f));

    // Preguntas que se contestan consultando la base de datos en ese momento
    private static async Task<RespuestaMoni?> DatoEnVivoAsync(AppDbContext db, string t)
    {
        var hoy = DateTime.Today;
        var manana = hoy.AddDays(1);
        var pideCifra = Tiene(t, "cuanto", "cuantos", "cuantas", "total", "monto", "dinero", "cual", "quien", "que producto", "dame", "dime");

        // Más vendido (últimos 30 días)
        if (Tiene(t, "mas vendido", "mas se vende", "mas vende", "producto estrella", "mas popular"))
        {
            var desde = hoy.AddDays(-30);
            var top = await db.DetallesVenta
                .Where(d => !d.Venta!.Anulada && d.Venta.Fecha >= desde)
                .GroupBy(d => d.Producto!.Nombre)
                .Select(g => new { Nombre = g.Key, Unidades = g.Sum(d => d.Cantidad) })
                .OrderByDescending(x => x.Unidades).FirstOrDefaultAsync();
            return top == null
                ? new("En los últimos 30 días no hay ventas para comparar.", "reportes")
                : new($"En los últimos 30 días, el producto más vendido es «{top.Nombre}» con {top.Unidades} unidades.", "reportes");
        }

        // Ventas de hoy
        if (Tiene(t, "vendi", "vendimos", "vendio", "se vendio", "ventas de hoy", "ventas hoy", "venta de hoy", "llevamos") ||
            (pideCifra && Tiene(t, "venta", "ventas") && Tiene(t, "hoy", "dia")))
        {
            var lineas = await db.DetallesVenta
                .Where(d => !d.Venta!.Anulada && d.Venta.Fecha >= hoy && d.Venta.Fecha < manana)
                .Select(d => new { d.VentaId, Importe = d.Cantidad * d.PrecioUnitario })
                .ToListAsync();
            var ventas = lineas.Select(l => l.VentaId).Distinct().Count();
            return ventas == 0
                ? new("Hoy todavía no hay ventas registradas. ¿Hacemos la primera?", "ventas")
                : new($"Hoy van {ventas} venta(s) por un total de {lineas.Sum(l => l.Importe):C}. " +
                      $"El ticket promedio es de {lineas.Sum(l => l.Importe) / ventas:C}.", "historial",
                      new[] { "¿Cuál es el producto más vendido?", "¿Qué productos se están agotando?" });
        }

        // Productos por agotarse (usa la predicción de la IA)
        if (Tiene(t, "agota", "agotando", "agotado", "stock bajo", "poco stock", "se acaba", "falta", "faltan", "reabastecer", "critico", "existencia"))
        {
            var urgentes = (await PrediccionInventario.CalcularAsync(db))
                .Where(p => p.DiasParaAgotarse is <= 7 || p.Stock <= DashboardViewModel.UmbralStockBajo)
                .Take(5).ToList();
            if (urgentes.Count == 0)
                return new("¡Buenas noticias! Ningún producto se agota en los próximos 7 días.", "asistente");

            var lista = string.Join("; ", urgentes.Select(p =>
                p.Stock == 0 ? $"{p.Producto}: agotado"
                : p.DiasParaAgotarse is int d ? $"{p.Producto}: {p.Stock} u., se agota {(d == 0 ? "hoy" : $"en {d} día(s)")}"
                : $"{p.Producto}: {p.Stock} u."));
            return new($"Estos productos necesitan atención: {lista}. En el Asistente IA puede crear la compra sugerida.", "asistente",
                       new[] { "¿Qué es la compra sugerida?" });
        }

        // Mejor cliente (últimos 30 días)
        if (Tiene(t, "mejor cliente", "cliente que mas", "quien compra mas", "cliente frecuente"))
        {
            var desde = hoy.AddDays(-30);
            var mejor = await db.DetallesVenta
                .Where(d => !d.Venta!.Anulada && d.Venta.Fecha >= desde && d.Venta.ClienteId != null)
                .GroupBy(d => d.Venta!.Cliente!.Nombre)
                .Select(g => new { Nombre = g.Key, Total = g.Sum(d => d.Cantidad * d.PrecioUnitario) })
                .OrderByDescending(x => x.Total).FirstOrDefaultAsync();
            return mejor == null
                ? new("En los últimos 30 días no hay ventas con cliente registrado.", "reportes")
                : new($"El mejor cliente de los últimos 30 días es {mejor.Nombre}, con {mejor.Total:C} en compras.", "reportes");
        }

        // Conteos generales
        if (pideCifra || Tiene(t, "hay"))
        {
            if (Tiene(t, "cliente")) return new($"Hay {await db.Clientes.CountAsync()} cliente(s) registrados.", "clientes");
            if (Tiene(t, "producto")) return new($"Hay {await db.Productos.CountAsync()} producto(s) en el catálogo, con {await db.Productos.SumAsync(p => p.Stock):#,0} unidades en bodega.", "productos");
            if (Tiene(t, "empleado", "cajero")) return new($"Hay {await db.Empleados.CountAsync(e => e.Activo)} empleado(s) activos.", "empleados");
            if (Tiene(t, "proveedor")) return new($"Hay {await db.Proveedores.CountAsync()} proveedor(es) registrados.", "proveedores");
            if (Tiene(t, "promocion", "descuento", "oferta"))
            {
                var vigentes = await db.Promociones.CountAsync(p => p.Activa && p.FechaInicio <= hoy && p.FechaFin >= hoy);
                return new($"Hay {vigentes} promoción(es) vigente(s) hoy.", "promociones");
            }
        }

        // Última venta
        if (Tiene(t, "ultima venta", "venta mas reciente"))
        {
            var ultima = await db.Ventas.Where(v => !v.Anulada).OrderByDescending(v => v.Fecha)
                .Select(v => new { v.VentaId, v.Fecha, Total = v.Detalles.Sum(d => d.Cantidad * d.PrecioUnitario) })
                .FirstOrDefaultAsync();
            return ultima == null
                ? new("Todavía no hay ventas registradas.", "ventas")
                : new($"La última venta es la #{ultima.VentaId}, del {ultima.Fecha:dd/MM/yyyy HH:mm}, por {ultima.Total:C}.", "historial");
        }

        // Fecha y hora
        if (Tiene(t, "que hora", "que dia", "fecha de hoy", "que fecha"))
            return new($"Hoy es {DateTime.Now:dddd d 'de' MMMM 'de' yyyy, h:mm tt}.");

        return null;
    }

    /// <summary>Minúsculas, sin tildes y sin signos: "¿Cómo ANULO?" → "como anulo".</summary>
    public static string Normalizar(string? texto)
    {
        if (string.IsNullOrWhiteSpace(texto)) return string.Empty;
        var sb = new StringBuilder();
        foreach (var c in texto.Trim().ToLowerInvariant().Normalize(NormalizationForm.FormD))
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.NonSpacingMark) continue;
            sb.Append(char.IsLetterOrDigit(c) ? c : ' ');
        }
        return string.Join(' ', sb.ToString().Split(' ', StringSplitOptions.RemoveEmptyEntries));
    }
}
