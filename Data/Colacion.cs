namespace MonitorVentas.Data;

/// <summary>
/// Colación de SQL Server que ignora mayúsculas y acentos ("jose" = "José", "lacteos" = "LÁCTEOS").
/// Se usa en las búsquedas del servidor y al revisar nombres duplicados.
/// </summary>
public static class Colacion
{
    public const string SinAcentos = "Modern_Spanish_CI_AI";
}
