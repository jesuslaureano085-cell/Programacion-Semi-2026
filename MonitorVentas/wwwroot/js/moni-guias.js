// =====================================================================
// GUÍAS PASO A PASO DE MONI
// Cada guía es una lista de pasos. Tipos de paso:
//   { abrir: 'modulo', texto }              → señala el ícono y espera a que se abra la ventana
//   { en: 'modulo', sel: 'css', texto }     → señala un elemento DENTRO de la ventana de ese módulo
//   esperar: '/Ruta'                        → el paso avanza solo cuando la ventana llega a esa página
//   final: true                             → último paso
// =====================================================================
(() => {
    'use strict';

    // Guía genérica para los catálogos sencillos (lista + nuevo + guardar)
    function catalogo(id, ruta, nombre, boton, campos, cierre) {
        return {
            titulo: 'Cómo usar ' + nombre,
            pasos: [
                { abrir: id, texto: `Primero abramos «${nombre}». Haga doble clic en este ícono, o pulse «Ábralo por mí».` },
                { en: id, sel: '#buscar', texto: 'Esta es la lista. Con el buscador de arriba se filtra al instante mientras escribe, sin importar tildes ni mayúsculas.' },
                { en: id, sel: '.page-header .btn-primary', texto: `Para registrar uno nuevo pulse «${boton}». Yo espero aquí.`, esperar: `/${ruta}/Create` },
                ...campos.map(([sel, texto]) => ({ en: id, sel, texto })),
                { en: id, sel: 'form button[type=submit]', texto: 'Pulse «Guardar». Si algún dato está mal, el campo se marca en rojo y le explica el motivo.', esperar: `^/${ruta}/?(Index)?$` },
                { en: id, sel: '#tabla', texto: cierre || '¡Listo! Ahí aparece el registro nuevo, con un mensaje verde de confirmación. Con «Editar» lo cambia y con «Eliminar» lo borra, si todavía no se ha usado.', final: true }
            ]
        };
    }

    window.MoniGuias = {
        clientes: catalogo('clientes', 'Clientes', 'Clientes', 'Nuevo cliente', [
            ['#Nombre', 'Hagamos un ejemplo. Escriba el nombre: «Ana López». Es obligatorio (por eso tiene el asterisco rojo).'],
            ['#Telefono', 'Ahora el teléfono, por ejemplo «7012-3456». Debe tener al menos 8 dígitos y no puede repetirse con otro cliente.'],
            ['#Direccion', 'La dirección es opcional; puede dejarla vacía.']
        ]),

        categorias: catalogo('categorias', 'Categorias', 'Categorías', 'Nueva categoría', [
            ['#Nombre', 'Escriba el nombre, por ejemplo «Panadería». No se puede repetir.'],
            ['#Descripcion', 'La descripción es opcional: sirve para aclarar qué productos van ahí.']
        ]),

        proveedores: catalogo('proveedores', 'Proveedores', 'Proveedores', 'Nuevo proveedor', [
            ['#Nombre', 'Escriba el nombre de la empresa, por ejemplo «Distribuidora El Sol».'],
            ['#Telefono', 'El teléfono es obligatorio. El contacto, el correo y la dirección son opcionales.']
        ]),

        empleados: catalogo('empleados', 'Empleados', 'Empleados', 'Nuevo empleado', [
            ['#Nombre', 'Escriba el nombre completo, por ejemplo «Carlos Méndez».'],
            ['#Dui', 'El DUI tiene el formato 00000000-0. Escriba solo los números: el guion se pone solo.'],
            ['#Cargo', 'Elija el cargo: Cajero, Supervisor, Bodeguero o Gerente. Los cajeros son los que pueden cobrar ventas.'],
            ['#Activo', '«Activo» queda marcado. Si algún día el empleado se va, se desmarca en lugar de borrarlo, para no perder su historial.']
        ], '¡Listo! El empleado ya aparece en la lista y en la caja como cajero.'),

        metodos: catalogo('metodos', 'MetodosPago', 'Métodos de pago', 'Nuevo método', [
            ['#Nombre', 'Escriba el nombre, por ejemplo «Billetera electrónica».'],
            ['#Activo', '«Disponible» indica si aparece en la caja. Si un método ya se usó, se desmarca en lugar de borrarlo.']
        ]),

        productos: {
            titulo: 'Cómo registrar un producto',
            pasos: [
                { abrir: 'productos', texto: 'Abramos «Productos». Haga doble clic en el ícono, o pulse «Ábralo por mí».' },
                { en: 'productos', sel: '#buscar', texto: 'Este es el catálogo. Puede buscar por nombre, código, categoría o proveedor, y filtrar por categoría. Las filas en rojo tienen 5 unidades o menos.' },
                { en: 'productos', sel: '.page-header .btn-primary', texto: 'Pulse «Nuevo producto».', esperar: '/Productos/Create' },
                { en: 'productos', sel: '#Codigo', texto: 'El código de barras es opcional, pero si lo escribe debe tener exactamente 13 dígitos (formato EAN-13).' },
                { en: 'productos', sel: '#Nombre', texto: 'Ejemplo: «Café molido 400 g».' },
                { en: 'productos', sel: '#CategoriaId', texto: 'Elija la categoría (por ejemplo, Bebidas)...' },
                { en: 'productos', sel: '#ProveedorId', texto: '...y el proveedor que lo surte. Si la lista está vacía, primero registre un proveedor.' },
                { en: 'productos', sel: '#Precio', texto: 'El precio de venta, por ejemplo 3.50. Debe ser mayor que cero.' },
                { en: 'productos', sel: '#Stock', texto: 'Las unidades que hay en bodega ahora. Después el stock se mueve solo con ventas, compras, devoluciones y ajustes.' },
                { en: 'productos', sel: 'form button[type=submit]', texto: 'Pulse «Guardar».', esperar: '^/Productos/?(Index)?$' },
                { en: 'productos', sel: '#tabla', texto: '¡Listo! El producto ya se puede vender en la caja. Un producto que ya tiene ventas no se puede eliminar, para cuidar el historial.', final: true }
            ]
        },

        ventas: {
            titulo: 'Cómo hacer una venta',
            pasos: [
                { abrir: 'ventas', texto: 'Abramos la caja: el ícono «Nueva venta». Doble clic, o pulse «Ábralo por mí».' },
                { en: 'ventas', sel: '#EmpleadoId', texto: 'Primero elija el cajero que cobra.' },
                { en: 'ventas', sel: '#ClienteId', texto: 'El cliente es opcional: si lo deja vacío, la venta queda como «Consumidor final».' },
                { en: 'ventas', sel: '#MetodoPagoId', texto: 'Elija cómo paga: efectivo, tarjeta o transferencia.' },
                { en: 'ventas', sel: '#pv-producto', texto: 'Ahora el producto: escriba parte del nombre y elíjalo de la lista, o escriba (o escanee) su código de barras.' },
                { en: 'ventas', sel: '#pv-cantidad', texto: 'Ponga la cantidad. El sistema no deja vender más de lo que hay en stock.' },
                { en: 'ventas', sel: '#pv-agregar', texto: 'Pulse «Agregar» (o Enter). Puede repetir con todos los productos que quiera.' },
                { en: 'ventas', sel: '#pv-tabla', texto: 'Aquí va la lista de lo que se lleva el cliente. Si un producto tiene promoción, se ve el precio tachado y el descuento.' },
                { en: 'ventas', sel: '#pv-total', texto: 'Este es el total a cobrar. Atajo: F2 regresa al buscador de productos.' },
                { en: 'ventas', sel: '#pv-cobrar', texto: 'Pulse «Cobrar». En ese momento el stock baja y el precio queda guardado tal como se cobró.', esperar: '/Ventas/Details' },
                { en: 'ventas', sel: '.ticket', texto: '¡Venta registrada! Este es el ticket: se puede imprimir. Desde aquí también puede registrar una devolución o anular la venta.', final: true }
            ]
        },

        anular: {
            titulo: 'Cómo anular una venta',
            pasos: [
                { abrir: 'historial', texto: 'Las ventas se anulan desde el «Historial de ventas». Ábralo con doble clic, o pulse «Ábralo por mí».' },
                { en: 'historial', sel: '#buscar', texto: 'Busque la venta por número, cliente o cajero, y cambie el período si es de otro día.' },
                { en: 'historial', sel: '#tabla a.btn-outline-primary', texto: 'Pulse «Ver ticket» en la venta que quiere anular.', esperar: '/Ventas/Details' },
                { en: 'historial', sel: '.btn-outline-danger', texto: 'Pulse «Anular venta». Si no aparece, es porque la venta ya tiene devoluciones o ya estaba anulada.', esperar: '/Ventas/Anular' },
                { en: 'historial', sel: 'form button[type=submit]', texto: 'Confirme. La venta NO se borra: queda tachada en el historial y el stock de sus productos se devuelve solo.', esperar: '/Ventas/Details' },
                { en: 'historial', sel: '.ticket', texto: '¡Listo! El ticket queda marcado como anulado y ya no cuenta en los reportes.', final: true }
            ]
        },

        devoluciones: {
            titulo: 'Cómo registrar una devolución',
            pasos: [
                { abrir: 'historial', texto: 'Una devolución sale de una venta. Abra el «Historial de ventas» (doble clic, o «Ábralo por mí»).' },
                { en: 'historial', sel: '#tabla a.btn-outline-primary', texto: 'Pulse «Ver ticket» en la venta del cliente.', esperar: '/Ventas/Details' },
                { en: 'historial', sel: 'a[href*="/Devoluciones/Create"]', texto: 'Pulse «Registrar devolución».', esperar: '/Devoluciones/Create' },
                { en: 'historial', sel: '.pd-cantidad', texto: 'Escriba cuántas unidades regresan de cada producto. No deja pasar de lo que se vendió (ni de lo ya devuelto).' },
                { en: 'historial', sel: '#Motivo', texto: 'Elija el motivo. Si es «Otro», la observación es obligatoria.' },
                { en: 'historial', sel: '#EmpleadoId', texto: 'Elija quién atiende la devolución.' },
                { en: 'historial', sel: '#Reintegrar', texto: 'Marcado: los productos vuelven al inventario. Desmarcado (vencido o dañado): se descartan y el stock no sube.' },
                { en: 'historial', sel: '#pd-total', texto: 'Este es el dinero a devolver, calculado con el precio que pagó el cliente (precio histórico).' },
                { en: 'historial', sel: 'form button[type=submit]', texto: 'Pulse «Registrar devolución».', esperar: '/Devoluciones/Details' },
                { en: 'historial', sel: '.ticket', texto: '¡Listo! Este es el comprobante de la devolución. La venta original ahora muestra sus devoluciones y el total neto.', final: true }
            ]
        },

        compras: {
            titulo: 'Cómo registrar una compra',
            pasos: [
                { abrir: 'compras', texto: 'Abramos «Compras» (doble clic, o «Ábralo por mí»).' },
                { en: 'compras', sel: '.page-header .btn-primary', texto: 'Pulse «Nueva compra».', esperar: '/Compras/Create' },
                { en: 'compras', sel: '#ProveedorId', texto: 'Elija primero el proveedor: la lista de productos solo mostrará los suyos.' },
                { en: 'compras', sel: '#NumeroFactura', texto: 'Escriba el número de la factura, por ejemplo «F-1001». No se puede repetir para el mismo proveedor.' },
                { en: 'compras', sel: '#EmpleadoId', texto: 'Elija quién recibe la mercadería.' },
                { en: 'compras', sel: '#pc-producto', texto: 'Busque el producto. Al elegirlo, el costo se llena con el último que se pagó.' },
                { en: 'compras', sel: '#pc-costo', texto: 'Revise el costo unitario. Si es mayor que el precio de venta, el sistema le avisa que perdería dinero.' },
                { en: 'compras', sel: '#pc-agregar', texto: 'Pulse «Agregar». Repita con cada producto de la factura.' },
                { en: 'compras', sel: '#pc-guardar', texto: 'Pulse «Registrar compra». El stock de cada producto sube en ese momento.', esperar: '/Compras/Details' },
                { en: 'compras', sel: '.comprobante', texto: '¡Listo! Este es el comprobante. Si se equivocó, la compra se puede anular mientras haya stock suficiente.', final: true }
            ]
        },

        promociones: {
            titulo: 'Cómo crear una promoción',
            pasos: [
                { abrir: 'promociones', texto: 'Abramos «Promociones» (doble clic, o «Ábralo por mí»).' },
                { en: 'promociones', sel: '.page-header .btn-primary', texto: 'Pulse «Nueva promoción».', esperar: '/Promociones/Create' },
                { en: 'promociones', sel: '#Nombre', texto: 'Póngale un nombre, por ejemplo «Semana del café».' },
                { en: 'promociones', sel: 'input[name=Alcance]', texto: 'Elija si el descuento es para un solo producto o para toda una categoría.' },
                { en: 'promociones', sel: '#Porcentaje', texto: 'El porcentaje de descuento, entre 1 y 90.' },
                { en: 'promociones', sel: '#FechaInicio', texto: 'Las fechas de inicio y fin. Fuera de ese rango, la promoción no se aplica.' },
                { en: 'promociones', sel: 'form button[type=submit]', texto: 'Pulse «Guardar».', esperar: '^/Promociones/?(Index)?$' },
                { en: 'promociones', sel: '#tabla', texto: '¡Listo! Si está vigente, la caja aplica sola el descuento y el ticket muestra «PROMO». Si un producto tiene dos promociones, gana la mejor.', final: true }
            ]
        },

        ajustes: {
            titulo: 'Cómo hacer un ajuste de inventario',
            pasos: [
                { abrir: 'ajustes', texto: 'Abramos «Ajustes de inventario» (doble clic, o «Ábralo por mí»).' },
                { en: 'ajustes', sel: '.page-header .btn-primary', texto: 'Pulse «Nuevo ajuste».', esperar: '/AjustesInventario/Create' },
                { en: 'ajustes', sel: '#ProductoId', texto: 'Elija el producto.' },
                { en: 'ajustes', sel: 'input[name=Tipo]', texto: 'Entrada suma unidades (por ejemplo, un conteo que salió de más). Salida las resta (vencidos, dañados, robo).' },
                { en: 'ajustes', sel: '#Motivo', texto: 'Elija el motivo y la cantidad. Una salida nunca puede dejar el stock en negativo.' },
                { en: 'ajustes', sel: 'form button[type=submit]', texto: 'Pulse «Registrar ajuste».', esperar: '^/AjustesInventario/?(Index)?$' },
                { en: 'ajustes', sel: '#tabla', texto: '¡Listo! El stock ya cambió y el ajuste queda registrado con su responsable. Si lo elimina, el stock se revierte.', final: true }
            ]
        },

        reportes: {
            titulo: 'Cómo usar los reportes',
            pasos: [
                { abrir: 'reportes', texto: 'Abramos «Reportes» (doble clic, o «Ábralo por mí»).' },
                { en: 'reportes', sel: '.reporte-tarjeta', texto: 'Hay 10 reportes agrupados por tema. Pulse el primero, «Ventas por día».', esperar: '/Reportes/Ver' },
                { en: 'reportes', sel: '#desde', texto: 'Elija el rango de fechas y pulse «Generar», o use los atajos: Hoy, Últimos 7 días, Este mes, Este año.' },
                { en: 'reportes', sel: '.resumen-caja', texto: 'Arriba van los números clave del período.' },
                { en: 'reportes', sel: '.grafico', texto: 'El gráfico de barras compara los valores de un vistazo.' },
                { en: 'reportes', sel: '.tabla-reporte', texto: 'Y la tabla completa, con la fila de totales al final.' },
                { en: 'reportes', sel: '.page-header .btn-primary', texto: '«Exportar a Excel» descarga un .xlsx con formato; «Imprimir» saca una hoja limpia con el encabezado de Monitor.', final: true }
            ]
        },

        asistente: {
            titulo: 'Cómo funciona el Asistente IA',
            pasos: [
                { abrir: 'asistente', texto: 'Abramos el «Asistente IA» (doble clic, o «Ábralo por mí»).' },
                { en: 'asistente', sel: '.resumen-caja', texto: 'Arriba: cuántos productos analizó, cuántos usan el modelo de IA y cuántos están críticos.' },
                { en: 'asistente', sel: '.ia-grafica', texto: 'Cada producto tiene su mini gráfica: en gris lo que se vendió los últimos 21 días y en verde punteado lo que la IA pronostica para los próximos 7.' },
                { en: 'asistente', sel: '#tabla', texto: 'La tabla dice cuánto se venderá, en cuántos días se agota y cuánto conviene comprar para 14 días. Los críticos salen primero, en rojo.' },
                { en: 'asistente', sel: '.ia-sugerencia .btn-primary', texto: '«Crear compra sugerida» abre Compras con todo ya lleno. Solo falta la factura.' },
                { en: 'asistente', sel: '.ia-pasos', texto: 'Aquí abajo se explica cómo piensa: ML.NET aprende la tendencia y el ciclo semanal con el modelo SSA.', final: true }
            ]
        }
    };
})();
