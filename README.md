# MONITOR · Sistema de ventas para supermercado

Proyecto final integrador de **Programación Computacional I**, Universidad Gerardo Barrios, Centro Regional de Usulután.

**Monitor** es una aplicación web para llevar las ventas, el inventario, las compras y las devoluciones de un supermercado.
Su interfaz es un **escritorio clásico estilo Windows 95**, diseñado desde cero, con ventanas, barra de tareas, menú Inicio y avisos.
Incluye un **Asistente con inteligencia artificial** que predice cuándo se agota cada producto y sugiere qué comprar.

---

## Tecnologías

| Capa | Tecnología |
|---|---|
| Lenguaje | C# (.NET 10) |
| Web | ASP.NET Core MVC + Razor |
| Datos | Entity Framework Core 10 (Code First + migraciones) |
| Base de datos | SQL Server (LocalDB en desarrollo) |
| Inteligencia artificial | ML.NET 6 (`Microsoft.ML.TimeSeries`, modelo SSA) |
| Interfaz | HTML, CSS, JavaScript, Bootstrap 5, jQuery Validation |

---

## Cómo ejecutarlo

**Requisitos:** Visual Studio 2026 con la carga de trabajo *Desarrollo de ASP.NET y web* y *SQL Server Express LocalDB*.

1. Clone el repositorio o descárguelo como ZIP.
2. Abra `MonitorVentas.slnx` en Visual Studio.
3. Presione **F5**. La primera vez el sistema crea solo la base de datos `MonitorVentasDb`
   con todas las tablas y los datos iniciales (métodos de pago y categorías).
4. Registre al menos un empleado, un proveedor y algunos productos.
5. **Opcional:** en **Asistente IA → Datos de demostración** se generan 90 días de historial para ver los reportes y la IA con datos.

> La cadena de conexión está en `appsettings.json` (`DefaultConnection`).
>
> Para dejar el sistema vacío otra vez: **Inicio → Restablecer sistema...** (borra todos los datos; pide escribir RESTABLECER).

---

## Módulos

| Tipo | Formulario | Tablas que usa |
|---|---|---|
| Básico | Clientes | Clientes |
| Básico | Categorías | Categorías |
| Básico | Proveedores | Proveedores |
| Básico | Empleados | Empleados |
| Básico | Métodos de pago | MetodosPago |
| Intermedio | Productos | Productos + Categorías + Proveedores |
| Intermedio | Ajustes de inventario | AjustesInventario + Productos + Empleados |
| Intermedio | Promociones | Promociones + Productos + Categorías |
| Avanzado | Ventas (punto de venta) | Ventas + DetallesVenta + Productos + Clientes + Empleados + MetodosPago + Promociones |
| Avanzado | Compras a proveedores | Compras + DetallesCompra + Proveedores + Productos + Empleados |
| Avanzado | Devoluciones | Devoluciones + DetallesDevolucion + DetallesVenta + Ventas + Productos + Empleados |

**Además:** dashboard (monitor de ventas en vivo), 10 reportes y el Asistente IA.

---

## Cómo cumple la bitácora

| Criterio | Dónde está |
|---|---|
| **Dashboard** | Escritorio con el *Monitor de ventas*: ventas de hoy, monto, clientes y stock bajo. Se actualiza solo cada 30 s. |
| **5 formularios CRUD básicos** | Clientes, Categorías, Proveedores, Empleados, Métodos de pago. |
| **Búsqueda interactiva** | Cada formulario filtra mientras se escribe, sin distinguir tildes ni mayúsculas, y también busca en el servidor. |
| **Validación** | En tres capas: navegador (DataAnnotations + jQuery), servidor (reglas de negocio, duplicados) y base de datos (índices únicos, *check constraints*, llaves foráneas). |
| **3 formularios CRUD intermedios** | Productos, Ajustes de inventario, Promociones. |
| **Creatividad e innovación** | Escritorio estilo 95 con intro animada propia, Asistente IA con ML.NET, compra sugerida automática, kardex y exportación a Excel. |

**Requisitos extra:** 3 formularios avanzados (encabezado + detalle con transacciones), 10 reportes, menú Inicio y menú contextual, barras de herramientas con íconos en cada ventana, barra de tareas, cuadros de diálogo y la IA.

---

## Reglas importantes del negocio

- **Precio histórico:** cada renglón de venta guarda el precio con el que se vendió (y el descuento aplicado). Si mañana cambia el precio del producto, las ventas viejas no cambian.
- **Encabezado / detalle:** Ventas, Compras y Devoluciones tienen un encabezado y varios renglones, y se guardan dentro de una **transacción**: o se guarda todo o nada.
- **Stock siempre correcto:** vender lo baja; comprar, devolver (si se reintegra) y los ajustes de entrada lo suben. Nunca puede quedar negativo.
- **Nada se borra a la fuerza:** las ventas y compras se **anulan** (el stock se revierte) y quedan en el historial. No se puede eliminar un registro que ya tiene movimientos (por ejemplo, un empleado con ventas): se inactiva.
- **Promociones:** se aplica automáticamente el mejor descuento vigente (por producto o por categoría).

---

## Reportes (10)

1. Ventas por día (vendido, devuelto y neto)
2. Productos más vendidos
3. Ventas por categoría
4. Ventas por cajero
5. Ventas por método de pago
6. Mejores clientes
7. Inventario valorizado
8. Kardex de producto (entradas, salidas y saldo)
9. Compras por proveedor
10. Devoluciones por motivo

Todos tienen filtro de fechas, gráfico de barras, totales, **impresión** y **exportación a Excel (.xlsx)**.

---

## Asistente IA

1. **Aprende** del historial: para cada producto arma la serie de unidades vendidas por día (últimos 90 días).
2. **Encuentra patrones** con el modelo **SSA (Singular Spectrum Analysis)** de ML.NET: la tendencia y el ciclo semanal.
3. **Pronostica** las ventas de los próximos 7 días con un rango de confianza del 90 %.
4. **Decide:** calcula en cuántos días se agota cada producto y cuánto comprar para cubrir 14 días más un margen de seguridad.
5. Con **Crear compra sugerida** se abre la compra ya llena; el escritorio avisa con un globo cuando un producto está crítico.

Si un producto tiene menos de 28 días de historial, se usa un promedio simple.

---

## Moni, el ayudante

Personaje original en pixel art (un pequeño monitor CRT) que vive en la esquina del escritorio.

- **Tutorial:** la primera vez recorre el escritorio ícono por ícono y explica cada módulo. Se puede saltar y repetir desde el menú Inicio.
- **Preguntas:** al hacerle clic responde dudas sobre el uso del sistema (base de conocimiento con búsqueda sin tildes) y datos en vivo de la base de datos: ventas de hoy, productos por agotarse, producto más vendido, mejor cliente, etc.
- Funciona sin internet ni servicios externos (`Data/AyudanteMoni.cs`, `wwwroot/js/moni.js`).

---

## Apagar Monitor

**Inicio → Apagar...** (o **Ctrl+Q**): Moni se despide, las ventanas se cierran y la pantalla se apaga como un monitor de tubo. Queda una pantalla negra con el botón **Encender**. También se puede **Reiniciar** para volver a ver la intro.

---

## Estructura del proyecto

```
Controllers/   Lógica de cada módulo (CRUD, ventas, compras, reportes, asistente)
Models/        Entidades de la base de datos y modelos de pantalla
Data/          AppDbContext, precios/promociones, predicción IA, exportación a Excel
Migrations/    Historial de cambios de la base de datos (EF Core)
Views/         Pantallas Razor
wwwroot/       CSS, JavaScript (escritorio, intro, punto de venta) y fuente VT323
```

---

## Integrantes

| N.º | Nombre | Carné |
|---|---|---|
| 1 | Carlos Alberto Sorto Garcia | USSS081126 |
| 2 | Saul Eduardo Salinas Escolero | USSS139126 |
| 3 | Jesus Alexander Laureano Menjivar | USSS077426 |
| 4 | Alma Iveth Parada Campos | USSS126626 |
| 5 | Melissa Isela Saravia Diaz | USSS148126 |
| 6 | Angel Adolfo Jimenez Jovel | USSS080626 |

Fuente tipográfica **VT323** de Peter Hull, con licencia SIL Open Font License.
