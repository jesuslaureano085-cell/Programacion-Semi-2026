using Microsoft.EntityFrameworkCore;
using MonitorVentas.Models;

namespace MonitorVentas.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // ----- Formularios básicos (una tabla) -----
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Proveedor> Proveedores => Set<Proveedor>();
    public DbSet<Empleado> Empleados => Set<Empleado>();
    public DbSet<MetodoPago> MetodosPago => Set<MetodoPago>();

    // ----- Intermedio -----
    public DbSet<Producto> Productos => Set<Producto>();

    public DbSet<AjusteInventario> AjustesInventario => Set<AjusteInventario>();
    public DbSet<Promocion> Promociones => Set<Promocion>();

    // ----- Avanzado (encabezado + detalle) -----
    public DbSet<Venta> Ventas => Set<Venta>();
    public DbSet<DetalleVenta> DetallesVenta => Set<DetalleVenta>();
    public DbSet<Compra> Compras => Set<Compra>();
    public DbSet<DetalleCompra> DetallesCompra => Set<DetalleCompra>();
    public DbSet<Devolucion> Devoluciones => Set<Devolucion>();
    public DbSet<DetalleDevolucion> DetallesDevolucion => Set<DetalleDevolucion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ---------- Relaciones de Productos ----------
        // No se puede borrar una categoría o un proveedor que tenga productos
        modelBuilder.Entity<Producto>()
            .HasOne(p => p.Categoria)
            .WithMany()
            .HasForeignKey(p => p.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Producto>()
            .HasOne(p => p.Proveedor)
            .WithMany()
            .HasForeignKey(p => p.ProveedorId)
            .OnDelete(DeleteBehavior.Restrict);

        // ---------- Relaciones de Ventas ----------
        // El cliente es opcional (vacío = consumidor final), pero si tiene ventas no se puede borrar
        modelBuilder.Entity<Venta>()
            .HasOne(v => v.Cliente)
            .WithMany(c => c.Ventas)
            .HasForeignKey(v => v.ClienteId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Venta>()
            .HasOne(v => v.Empleado)
            .WithMany()
            .HasForeignKey(v => v.EmpleadoId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Venta>()
            .HasOne(v => v.MetodoPago)
            .WithMany()
            .HasForeignKey(v => v.MetodoPagoId)
            .OnDelete(DeleteBehavior.Restrict);

        // No se puede borrar un producto que ya fue vendido
        modelBuilder.Entity<DetalleVenta>()
            .HasOne(d => d.Producto)
            .WithMany(p => p.Detalles)
            .HasForeignKey(d => d.ProductoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Los renglones pertenecen a su venta
        modelBuilder.Entity<DetalleVenta>()
            .HasOne(d => d.Venta)
            .WithMany(v => v.Detalles)
            .HasForeignKey(d => d.VentaId)
            .OnDelete(DeleteBehavior.Cascade);

        // ---------- Ajustes de inventario ----------
        // No se puede borrar un producto o un empleado que tenga ajustes registrados
        modelBuilder.Entity<AjusteInventario>()
            .HasOne(a => a.Producto)
            .WithMany()
            .HasForeignKey(a => a.ProductoId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AjusteInventario>()
            .HasOne(a => a.Empleado)
            .WithMany()
            .HasForeignKey(a => a.EmpleadoId)
            .OnDelete(DeleteBehavior.Restrict);

        // ---------- Promociones ----------
        // Si se borra el producto o la categoría, sus promociones se borran con él
        modelBuilder.Entity<Promocion>()
            .HasOne(p => p.Producto)
            .WithMany()
            .HasForeignKey(p => p.ProductoId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Promocion>()
            .HasOne(p => p.Categoria)
            .WithMany()
            .HasForeignKey(p => p.CategoriaId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        // ---------- Compras (encabezado + detalle) ----------
        modelBuilder.Entity<Compra>().HasOne(c => c.Proveedor).WithMany().HasForeignKey(c => c.ProveedorId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Compra>().HasOne(c => c.Empleado).WithMany().HasForeignKey(c => c.EmpleadoId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<DetalleCompra>().HasOne(d => d.Compra).WithMany(c => c.Detalles).HasForeignKey(d => d.CompraId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<DetalleCompra>().HasOne(d => d.Producto).WithMany().HasForeignKey(d => d.ProductoId).OnDelete(DeleteBehavior.Restrict);
        // Una misma factura de un proveedor no se registra dos veces (las anuladas no cuentan)
        modelBuilder.Entity<Compra>().HasIndex(c => new { c.ProveedorId, c.NumeroFactura }).IsUnique().HasFilter("[Anulada] = 0");
        modelBuilder.Entity<Compra>().HasIndex(c => c.Fecha);

        // ---------- Devoluciones (encabezado + detalle) ----------
        modelBuilder.Entity<Devolucion>().HasOne(d => d.Venta).WithMany().HasForeignKey(d => d.VentaId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Devolucion>().HasOne(d => d.Empleado).WithMany().HasForeignKey(d => d.EmpleadoId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<DetalleDevolucion>().HasOne(d => d.Devolucion).WithMany(x => x.Detalles).HasForeignKey(d => d.DevolucionId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<DetalleDevolucion>().HasOne(d => d.DetalleVenta).WithMany().HasForeignKey(d => d.DetalleVentaId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Devolucion>().HasIndex(d => d.Fecha);

        // ---------- Valores únicos (no se permiten duplicados) ----------
        modelBuilder.Entity<Cliente>().HasIndex(c => c.Telefono).IsUnique();
        modelBuilder.Entity<Categoria>().HasIndex(c => c.Nombre).IsUnique();
        modelBuilder.Entity<Proveedor>().HasIndex(p => p.Nombre).IsUnique();
        modelBuilder.Entity<Empleado>().HasIndex(e => e.Dui).IsUnique();
        modelBuilder.Entity<MetodoPago>().HasIndex(m => m.Nombre).IsUnique();
        modelBuilder.Entity<Producto>().HasIndex(p => p.Codigo).IsUnique().HasFilter("[Codigo] IS NOT NULL");
        modelBuilder.Entity<Venta>().HasIndex(v => v.Fecha);

        // ---------- Reglas también en la base de datos (no se pueden saltar) ----------
        modelBuilder.Entity<Producto>().ToTable(t =>
        {
            t.HasCheckConstraint("CK_Productos_Precio", "[Precio] > 0");
            t.HasCheckConstraint("CK_Productos_Stock", "[Stock] >= 0");
            t.HasCheckConstraint("CK_Productos_Codigo", "[Codigo] IS NULL OR ([Codigo] NOT LIKE '%[^0-9]%' AND LEN([Codigo]) = 13)");
        });

        modelBuilder.Entity<DetalleVenta>().ToTable(t =>
        {
            t.HasCheckConstraint("CK_DetallesVenta_Cantidad", "[Cantidad] > 0");
            t.HasCheckConstraint("CK_DetallesVenta_Precio", "[PrecioUnitario] > 0");
        });

        modelBuilder.Entity<AjusteInventario>().ToTable(t =>
        {
            t.HasCheckConstraint("CK_AjustesInventario_Cantidad", "[Cantidad] > 0");
            t.HasCheckConstraint("CK_AjustesInventario_Tipo", "[Tipo] IN ('Entrada', 'Salida')");
        });

        modelBuilder.Entity<Promocion>().ToTable(t =>
        {
            t.HasCheckConstraint("CK_Promociones_Porcentaje", "[Porcentaje] BETWEEN 1 AND 90");
            t.HasCheckConstraint("CK_Promociones_Fechas", "[FechaFin] >= [FechaInicio]");
            // Exactamente uno: o un producto o una categoría
            t.HasCheckConstraint("CK_Promociones_Destino",
                "([ProductoId] IS NOT NULL AND [CategoriaId] IS NULL) OR ([ProductoId] IS NULL AND [CategoriaId] IS NOT NULL)");
        });

        modelBuilder.Entity<DetalleVenta>().ToTable(t =>
        {
            t.HasCheckConstraint("CK_DetallesVenta_Descuento", "[DescuentoPorcentaje] BETWEEN 0 AND 90");
        });

        modelBuilder.Entity<DetalleCompra>().ToTable(t =>
        {
            t.HasCheckConstraint("CK_DetallesCompra_Cantidad", "[Cantidad] > 0");
            t.HasCheckConstraint("CK_DetallesCompra_Costo", "[CostoUnitario] > 0");
        });

        modelBuilder.Entity<DetalleDevolucion>().ToTable(t =>
        {
            t.HasCheckConstraint("CK_DetallesDevolucion_Cantidad", "[Cantidad] > 0");
        });

        modelBuilder.Entity<Empleado>().ToTable(t =>
        {
            t.HasCheckConstraint("CK_Empleados_Dui", "[Dui] LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]-[0-9]'");
        });

        // ---------- Datos iniciales (catálogos de referencia) ----------
        modelBuilder.Entity<MetodoPago>().HasData(MetodosPagoIniciales());
        modelBuilder.Entity<Categoria>().HasData(CategoriasIniciales());
    }

    // Datos iniciales: se crean con la base de datos y se restauran en "Restablecer sistema"
    public static MetodoPago[] MetodosPagoIniciales() => new[]
    {
        new MetodoPago { MetodoPagoId = 1, Nombre = "Efectivo", Descripcion = "Pago en caja con billetes y monedas.", Activo = true },
        new MetodoPago { MetodoPagoId = 2, Nombre = "Tarjeta", Descripcion = "Tarjeta de crédito o débito.", Activo = true },
        new MetodoPago { MetodoPagoId = 3, Nombre = "Transferencia", Descripcion = "Transferencia bancaria o pago móvil.", Activo = true }
    };

    public static Categoria[] CategoriasIniciales() => new[]
    {
        new Categoria { CategoriaId = 1, Nombre = "Abarrotes", Descripcion = "Arroz, frijol, aceite, azúcar y granos básicos." },
        new Categoria { CategoriaId = 2, Nombre = "Bebidas", Descripcion = "Agua, gaseosas, jugos y café." },
        new Categoria { CategoriaId = 3, Nombre = "Lácteos", Descripcion = "Leche, queso, crema y yogur." },
        new Categoria { CategoriaId = 4, Nombre = "Limpieza", Descripcion = "Detergentes, jabón y artículos del hogar." }
    };
}
