using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ModificarDevocionalCancion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DevocionalCancion");

            migrationBuilder.CreateTable(
                name: "DevocionalCanciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DevocionalId = table.Column<int>(type: "int", nullable: false),
                    CancionId = table.Column<int>(type: "int", nullable: false),
                    PosicionCancion = table.Column<int>(type: "int", nullable: false),
                    AcordesFinales = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevocionalCanciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DevocionalCanciones_Canciones_CancionId",
                        column: x => x.CancionId,
                        principalTable: "Canciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DevocionalCanciones_Devocionales_DevocionalId",
                        column: x => x.DevocionalId,
                        principalTable: "Devocionales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DevocionalCanciones_CancionId",
                table: "DevocionalCanciones",
                column: "CancionId");

            migrationBuilder.CreateIndex(
                name: "IX_DevocionalCanciones_DevocionalId",
                table: "DevocionalCanciones",
                column: "DevocionalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DevocionalCanciones");

            migrationBuilder.CreateTable(
                name: "DevocionalCancion",
                columns: table => new
                {
                    CancionesId = table.Column<int>(type: "int", nullable: false),
                    DevocionalesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevocionalCancion", x => new { x.CancionesId, x.DevocionalesId });
                    table.ForeignKey(
                        name: "FK_DevocionalCancion_Canciones_CancionesId",
                        column: x => x.CancionesId,
                        principalTable: "Canciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DevocionalCancion_Devocionales_DevocionalesId",
                        column: x => x.DevocionalesId,
                        principalTable: "Devocionales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DevocionalCancion_DevocionalesId",
                table: "DevocionalCancion",
                column: "DevocionalesId");
        }
    }
}
