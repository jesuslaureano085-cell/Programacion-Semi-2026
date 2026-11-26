using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using MonitorVentas.Data;

// Formato de El Salvador: punto decimal y dólar ($). Así "1.25" siempre es un dólar
// con veinticinco centavos, sin importar la configuración regional de la PC.
CultureInfo cultura;
try { cultura = (CultureInfo)CultureInfo.GetCultureInfo("es-SV").Clone(); }
catch (CultureNotFoundException) { cultura = (CultureInfo)CultureInfo.InvariantCulture.Clone(); } // equipos sin datos regionales
cultura.NumberFormat.NumberDecimalSeparator = ".";
cultura.NumberFormat.NumberGroupSeparator = ",";
cultura.NumberFormat.CurrencyDecimalSeparator = ".";
cultura.NumberFormat.CurrencyGroupSeparator = ",";
cultura.NumberFormat.CurrencySymbol = "$";
CultureInfo.DefaultThreadCurrentCulture = cultura;
CultureInfo.DefaultThreadCurrentUICulture = cultura;

var builder = WebApplication.CreateBuilder(args);

// Servicios: MVC (controladores + vistas)
builder.Services.AddControllersWithViews();

// Entity Framework Core + SQL Server (cadena "DefaultConnection" en appsettings.json)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Al iniciar, se crea o actualiza la base de datos con las migraciones pendientes.
// Así basta con abrir el proyecto y presionar F5 (no hace falta escribir Update-Database).
// Si la base de datos no responde, el sitio no se cae: muestra una página con el motivo.
string? errorBaseDatos = null;
try
{
    using var alcance = app.Services.CreateScope();
    alcance.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
}
catch (Exception ex)
{
    var causa = ex;
    while (causa.InnerException != null) causa = causa.InnerException;
    errorBaseDatos = causa.Message;
    app.Logger.LogError(ex, "No se pudo preparar la base de datos.");
}

if (errorBaseDatos != null)
{
    app.Run(async contexto =>
    {
        contexto.Response.StatusCode = 503;
        contexto.Response.ContentType = "text/plain; charset=utf-8";
        await contexto.Response.WriteAsync(
            "MONITOR - No se pudo conectar o preparar la base de datos.\n\n" +
            "Revise la cadena de conexión \"DefaultConnection\" en appsettings.json " +
            "y que SQL Server (LocalDB) esté instalado.\n\nDetalle: " + errorBaseDatos);
    });
}

// Fuera de desarrollo, los errores muestran una página amigable y se exige HTTPS
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(cultura),
    SupportedCultures = new[] { cultura },
    SupportedUICultures = new[] { cultura }
});

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
