using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonitorVentas.Migrations
{
    /// <inheritdoc />
    public partial class Beta3AjustesPromociones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DescuentoPorcentaje",
                table: "DetallesVenta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AjustesInventario",
                columns: table => new
                {
                    AjusteInventarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AjustesInventario", x => x.AjusteInventarioId);
                    table.CheckConstraint("CK_AjustesInventario_Cantidad", "[Cantidad] > 0");
                    table.CheckConstraint("CK_AjustesInventario_Tipo", "[Tipo] IN ('Entrada', 'Salida')");
                    table.ForeignKey(
                        name: "FK_AjustesInventario_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "EmpleadoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AjustesInventario_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Promociones",
                columns: table => new
                {
                    PromocionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: true),
                    CategoriaId = table.Column<int>(type: "int", nullable: true),
                    Porcentaje = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: false),
                    Activa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promociones", x => x.PromocionId);
                    table.CheckConstraint("CK_Promociones_Destino", "([ProductoId] IS NOT NULL AND [CategoriaId] IS NULL) OR ([ProductoId] IS NULL AND [CategoriaId] IS NOT NULL)");
                    table.CheckConstraint("CK_Promociones_Fechas", "[FechaFin] >= [FechaInicio]");
                    table.CheckConstraint("CK_Promociones_Porcentaje", "[Porcentaje] BETWEEN 1 AND 90");
                    table.ForeignKey(
                        name: "FK_Promociones_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Promociones_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_DetallesVenta_Descuento",
                table: "DetallesVenta",
                sql: "[DescuentoPorcentaje] BETWEEN 0 AND 90");

            migrationBuilder.CreateIndex(
                name: "IX_AjustesInventario_EmpleadoId",
                table: "AjustesInventario",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_AjustesInventario_ProductoId",
                table: "AjustesInventario",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Promociones_CategoriaId",
                table: "Promociones",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Promociones_ProductoId",
                table: "Promociones",
                column: "ProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AjustesInventario");

            migrationBuilder.DropTable(
                name: "Promociones");

            migrationBuilder.DropCheckConstraint(
                name: "CK_DetallesVenta_Descuento",
                table: "DetallesVenta");

            migrationBuilder.DropColumn(
                name: "DescuentoPorcentaje",
                table: "DetallesVenta");
        }
    }
}
