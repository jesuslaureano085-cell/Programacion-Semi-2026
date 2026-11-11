using Microsoft.EntityFrameworkCore;
using webapp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Conecta el "puente" (MyDbContext) con SQL Server usando la cadena "DefaultConnection" de appsettings.json
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Permite abrir los archivos de wwwroot (html, imágenes) desde el navegador
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
