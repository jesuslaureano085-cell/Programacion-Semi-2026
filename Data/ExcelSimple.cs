using System.Globalization;
using System.IO.Compression;
using System.Security;
using System.Text;
using MonitorVentas.Models;

namespace MonitorVentas.Data;

/// <summary>
/// Crea un archivo de Excel (.xlsx) real a partir de un Reporte, sin paquetes externos.
/// Un .xlsx es un ZIP con varios archivos XML adentro; aquí se escriben a mano los mínimos.
/// Así Excel lo abre con columnas, formato de moneda y fechas, sin importar la configuración regional.
/// </summary>
public static class ExcelSimple
{
    // Índices de estilo (deben coincidir con el orden de <cellXfs> en Estilos)
    private const int Normal = 0, Entero = 1, Moneda = 2, Fecha = 3, FechaHora = 4, Porcentaje = 5,
                      Encabezado = 6, Titulo = 7, NegritaTexto = 8, NegritaEntero = 9, NegritaMoneda = 10;

    public static byte[] Crear(Reporte r)
    {
        using var memoria = new MemoryStream();
        using (var zip = new ZipArchive(memoria, ZipArchiveMode.Create, leaveOpen: true))
        {
            Agregar(zip, "[Content_Types].xml", TiposContenido);
            Agregar(zip, "_rels/.rels", Relaciones);
            Agregar(zip, "xl/workbook.xml", Libro);
            Agregar(zip, "xl/_rels/workbook.xml.rels", RelacionesLibro);
            Agregar(zip, "xl/styles.xml", Estilos);
            Agregar(zip, "xl/worksheets/sheet1.xml", Hoja(r));
        }
        return memoria.ToArray();
    }

    private static void Agregar(ZipArchive zip, string ruta, string contenido)
    {
        var entrada = zip.CreateEntry(ruta, CompressionLevel.Optimal);
        using var escritor = new StreamWriter(entrada.Open(), new UTF8Encoding(false));
        escritor.Write(contenido);
    }

    // ---------- La hoja con los datos ----------
    private static string Hoja(Reporte r)
    {
        var filas = new StringBuilder();
        var n = 0;

        // Título y rango
        n++; filas.Append(Fila(n, Texto(n, 0, r.Titulo, Titulo)));
        var subtitulo = (r.UsaFechas ? $"Del {r.Desde:dd/MM/yyyy} al {r.Hasta:dd/MM/yyyy} · " : "")
                        + $"Generado el {DateTime.Now:dd/MM/yyyy HH:mm}";
        n++; filas.Append(Fila(n, Texto(n, 0, subtitulo, Normal)));
        n++; // fila en blanco

        // Encabezados
        n++;
        filas.Append(Fila(n, string.Concat(r.Columnas.Select((c, i) => Texto(n, i, c.Titulo, Encabezado)))));

        // Datos
        foreach (var f in r.Filas)
        {
            n++;
            filas.Append(Fila(n, string.Concat(r.Columnas.Select((c, i) => Celda(n, i, f.Celdas[i], c.Tipo, false)))));
        }

        // Totales
        if (r.TieneTotales && r.Filas.Any())
        {
            n++;
            filas.Append(Fila(n, string.Concat(r.Columnas.Select((c, i) =>
                i == 0 ? Texto(n, i, "TOTAL", NegritaTexto)
                       : c.Total ? Celda(n, i, r.Sumar(i), c.Tipo, true) : ""))));
        }

        // Resumen debajo de la tabla
        if (r.Resumen.Any())
        {
            n++;
            foreach (var (etiqueta, valor) in r.Resumen)
            {
                n++;
                filas.Append(Fila(n, Texto(n, 0, etiqueta, NegritaTexto) + Texto(n, 1, valor, Normal)));
            }
        }

        // Ancho de columnas: las de texto más anchas
        var columnas = string.Concat(r.Columnas.Select((c, i) =>
            $"<col min=\"{i + 1}\" max=\"{i + 1}\" width=\"{(c.EsNumero ? 16 : c.Tipo == TipoColumna.Texto ? 30 : 18)}\" customWidth=\"1\"/>"));

        return "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
               "<worksheet xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\">" +
               $"<cols>{columnas}</cols><sheetData>{filas}</sheetData></worksheet>";
    }

    private static string Fila(int n, string celdas) => $"<row r=\"{n}\">{celdas}</row>";

    // Referencia tipo "A1", "B5"...
    private static string Ref(int fila, int columna)
    {
        var letras = "";
        for (var c = columna + 1; c > 0; c = (c - 1) / 26)
            letras = (char)('A' + (c - 1) % 26) + letras;
        return letras + fila;
    }

    private static string Texto(int fila, int columna, string texto, int estilo) =>
        $"<c r=\"{Ref(fila, columna)}\" t=\"inlineStr\" s=\"{estilo}\"><is><t xml:space=\"preserve\">{SecurityElement.Escape(texto)}</t></is></c>";

    private static string Numero(int fila, int columna, double valor, int estilo) =>
        $"<c r=\"{Ref(fila, columna)}\" s=\"{estilo}\"><v>{valor.ToString(CultureInfo.InvariantCulture)}</v></c>";

    private static string Celda(int fila, int columna, object? valor, TipoColumna tipo, bool negrita)
    {
        if (valor == null) return "";
        if (valor is DateTime fecha)
            return Numero(fila, columna, fecha.ToOADate(), tipo == TipoColumna.FechaHora ? FechaHora : Fecha);

        return tipo switch
        {
            TipoColumna.Moneda => Numero(fila, columna, Convert.ToDouble(valor), negrita ? NegritaMoneda : Moneda),
            TipoColumna.Entero => Numero(fila, columna, Convert.ToDouble(valor), negrita ? NegritaEntero : Entero),
            TipoColumna.Porcentaje => Numero(fila, columna, Convert.ToDouble(valor), Porcentaje),
            _ => Texto(fila, columna, Convert.ToString(valor) ?? "", negrita ? NegritaTexto : Normal)
        };
    }

    // ---------- Archivos fijos del paquete ----------
    private const string TiposContenido =
        "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
        "<Types xmlns=\"http://schemas.openxmlformats.org/package/2006/content-types\">" +
        "<Default Extension=\"rels\" ContentType=\"application/vnd.openxmlformats-package.relationships+xml\"/>" +
        "<Default Extension=\"xml\" ContentType=\"application/xml\"/>" +
        "<Override PartName=\"/xl/workbook.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml\"/>" +
        "<Override PartName=\"/xl/worksheets/sheet1.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml\"/>" +
        "<Override PartName=\"/xl/styles.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml\"/>" +
        "</Types>";

    private const string Relaciones =
        "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
        "<Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\">" +
        "<Relationship Id=\"rId1\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument\" Target=\"xl/workbook.xml\"/>" +
        "</Relationships>";

    private const string Libro =
        "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
        "<workbook xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\" " +
        "xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\">" +
        "<sheets><sheet name=\"Reporte\" sheetId=\"1\" r:id=\"rId1\"/></sheets></workbook>";

    private const string RelacionesLibro =
        "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
        "<Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\">" +
        "<Relationship Id=\"rId1\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet\" Target=\"worksheets/sheet1.xml\"/>" +
        "<Relationship Id=\"rId2\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles\" Target=\"styles.xml\"/>" +
        "</Relationships>";

    // Formatos: 164 = moneda $, 165 = fecha, 166 = fecha y hora, 167 = porcentaje "86.7 %"
    private const string Estilos =
        "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
        "<styleSheet xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\">" +
        "<numFmts count=\"4\">" +
        "<numFmt numFmtId=\"164\" formatCode=\"&quot;$&quot;#,##0.00\"/>" +
        "<numFmt numFmtId=\"165\" formatCode=\"dd/mm/yyyy\"/>" +
        "<numFmt numFmtId=\"166\" formatCode=\"dd/mm/yyyy hh:mm\"/>" +
        "<numFmt numFmtId=\"167\" formatCode=\"0.0&quot; %&quot;\"/>" +
        "</numFmts>" +
        "<fonts count=\"3\">" +
        "<font><sz val=\"11\"/><name val=\"Calibri\"/></font>" +
        "<font><b/><sz val=\"11\"/><name val=\"Calibri\"/></font>" +
        "<font><b/><sz val=\"14\"/><name val=\"Calibri\"/></font>" +
        "</fonts>" +
        "<fills count=\"3\">" +
        "<fill><patternFill patternType=\"none\"/></fill>" +
        "<fill><patternFill patternType=\"gray125\"/></fill>" +
        "<fill><patternFill patternType=\"solid\"><fgColor rgb=\"FFC8C4B8\"/><bgColor indexed=\"64\"/></patternFill></fill>" +
        "</fills>" +
        "<borders count=\"1\"><border><left/><right/><top/><bottom/><diagonal/></border></borders>" +
        "<cellStyleXfs count=\"1\"><xf numFmtId=\"0\" fontId=\"0\" fillId=\"0\" borderId=\"0\"/></cellStyleXfs>" +
        "<cellXfs count=\"11\">" +
        "<xf numFmtId=\"0\" fontId=\"0\" fillId=\"0\" borderId=\"0\" xfId=\"0\"/>" +                              // 0 normal
        "<xf numFmtId=\"3\" fontId=\"0\" fillId=\"0\" borderId=\"0\" xfId=\"0\" applyNumberFormat=\"1\"/>" +       // 1 entero
        "<xf numFmtId=\"164\" fontId=\"0\" fillId=\"0\" borderId=\"0\" xfId=\"0\" applyNumberFormat=\"1\"/>" +     // 2 moneda
        "<xf numFmtId=\"165\" fontId=\"0\" fillId=\"0\" borderId=\"0\" xfId=\"0\" applyNumberFormat=\"1\"/>" +     // 3 fecha
        "<xf numFmtId=\"166\" fontId=\"0\" fillId=\"0\" borderId=\"0\" xfId=\"0\" applyNumberFormat=\"1\"/>" +     // 4 fecha y hora
        "<xf numFmtId=\"167\" fontId=\"0\" fillId=\"0\" borderId=\"0\" xfId=\"0\" applyNumberFormat=\"1\"/>" +     // 5 porcentaje
        "<xf numFmtId=\"0\" fontId=\"1\" fillId=\"2\" borderId=\"0\" xfId=\"0\" applyFont=\"1\" applyFill=\"1\"/>" + // 6 encabezado
        "<xf numFmtId=\"0\" fontId=\"2\" fillId=\"0\" borderId=\"0\" xfId=\"0\" applyFont=\"1\"/>" +               // 7 título
        "<xf numFmtId=\"0\" fontId=\"1\" fillId=\"0\" borderId=\"0\" xfId=\"0\" applyFont=\"1\"/>" +               // 8 negrita
        "<xf numFmtId=\"3\" fontId=\"1\" fillId=\"0\" borderId=\"0\" xfId=\"0\" applyFont=\"1\" applyNumberFormat=\"1\"/>" +   // 9
        "<xf numFmtId=\"164\" fontId=\"1\" fillId=\"0\" borderId=\"0\" xfId=\"0\" applyFont=\"1\" applyNumberFormat=\"1\"/>" + // 10
        "</cellXfs>" +
        "<cellStyles count=\"1\"><cellStyle name=\"Normal\" xfId=\"0\" builtinId=\"0\"/></cellStyles>" +
        "</styleSheet>";
}
