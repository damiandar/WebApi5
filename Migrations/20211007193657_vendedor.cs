using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyRepositorio.Migrations
{
    public partial class vendedor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<int>(
                name: "VendedorId",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Vendedor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendedor", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_VendedorId",
                table: "Productos",
                column: "VendedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Vendedor_VendedorId",
                table: "Productos",
                column: "VendedorId",
                principalTable: "Vendedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Vendedor_VendedorId",
                table: "Productos");

            migrationBuilder.DropTable(
                name: "Vendedor");

            migrationBuilder.DropIndex(
                name: "IX_Productos_VendedorId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "VendedorId",
                table: "Productos");
        }
    }
}
