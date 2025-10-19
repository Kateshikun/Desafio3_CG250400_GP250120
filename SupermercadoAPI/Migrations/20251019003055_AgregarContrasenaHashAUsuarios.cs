using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupermercadoAPI.Migrations
{
    /// <inheritdoc />
    public partial class AgregarContrasenaHashAUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Productos_productoidProducto",
                table: "Inventarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Categorias_categoriaidCategoria",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Inventarios_productoidProducto",
                table: "Inventarios");

            migrationBuilder.DropColumn(
                name: "productoidProducto",
                table: "Inventarios");

            migrationBuilder.RenameColumn(
                name: "cantidadActual",
                table: "Inventarios",
                newName: "CantidadActual");

            migrationBuilder.AlterColumn<int>(
                name: "categoriaidCategoria",
                table: "Productos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_idProducto",
                table: "Inventarios",
                column: "idProducto",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Productos_idProducto",
                table: "Inventarios",
                column: "idProducto",
                principalTable: "Productos",
                principalColumn: "idProducto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Categorias_categoriaidCategoria",
                table: "Productos",
                column: "categoriaidCategoria",
                principalTable: "Categorias",
                principalColumn: "idCategoria");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventarios_Productos_idProducto",
                table: "Inventarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Categorias_categoriaidCategoria",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Inventarios_idProducto",
                table: "Inventarios");

            migrationBuilder.RenameColumn(
                name: "CantidadActual",
                table: "Inventarios",
                newName: "cantidadActual");

            migrationBuilder.AlterColumn<int>(
                name: "categoriaidCategoria",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "productoidProducto",
                table: "Inventarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_productoidProducto",
                table: "Inventarios",
                column: "productoidProducto");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventarios_Productos_productoidProducto",
                table: "Inventarios",
                column: "productoidProducto",
                principalTable: "Productos",
                principalColumn: "idProducto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Categorias_categoriaidCategoria",
                table: "Productos",
                column: "categoriaidCategoria",
                principalTable: "Categorias",
                principalColumn: "idCategoria",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
