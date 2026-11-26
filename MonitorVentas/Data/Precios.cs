using Microsoft.EntityFrameworkCore;

namespace MonitorVentas.Data;

/// <summary>
/// Calcula los descuentos de las promociones vigentes HOY.
/// Lo usan la caja (para mostrar precios) y el servidor (para cobrar),
/// así los dos siempre calculan lo mismo.
/// </summary>
public static class Precios
{
    /// <summary>
    /// Devuelve, por ProductoId, el mayor descuento (%) vigente hoy.
    /// Un producto puede tener promoción propia o por su categoría: se usa la mejor.
    /// </summary>
    public static async Task<Dictionary<int, int>> DescuentosVigentesAsync(AppDbContext db, IEnumerable<int>? productoIds = null)
    {
        var hoy = DateTime.Today;
        var promos = await db.Promociones
            .AsNoTracking()
            .Where(p => p.Activa && p.FechaInicio <= hoy && p.FechaFin >= hoy)
            .Select(p => new { p.ProductoId, p.CategoriaId, p.Porcentaje })
            .ToListAsync();

        var resultado = new Dictionary<int, int>();
        if (promos.Count == 0) return resultado;

        var consulta = db.Productos.AsNoTracking();
        if (productoIds != null)
        {
            var ids = productoIds.ToList();
            consulta = consulta.Where(p => ids.Contains(p.ProductoId));
        }
        var productos = await consulta.Select(p => new { p.ProductoId, p.CategoriaId }).ToListAsync();

        foreach (var p in productos)
        {
            var mejor = promos
                .Where(x => x.ProductoId == p.ProductoId || x.CategoriaId == p.CategoriaId)
                .Select(x => x.Porcentaje)
                .DefaultIfEmpty(0)
                .Max();
            if (mejor > 0) resultado[p.ProductoId] = mejor;
        }
        return resultado;
    }

    /// <summary>Precio con descuento, redondeado a centavos.</summary>
    public static decimal ConDescuento(decimal precio, int porcentaje) =>
        porcentaje <= 0 ? precio : Math.Max(0.01m, Math.Round(precio * (100 - porcentaje) / 100m, 2, MidpointRounding.AwayFromZero));
}
