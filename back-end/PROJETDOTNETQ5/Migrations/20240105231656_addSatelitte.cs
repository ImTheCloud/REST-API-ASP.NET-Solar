using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROJETDOTNETQ5.Migrations
{
    /// <inheritdoc />
    public partial class addSatelitte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Satellites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Orbit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mass = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CelestialObjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Satellites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Satellites_CelestialObjects_CelestialObjectId",
                        column: x => x.CelestialObjectId,
                        principalTable: "CelestialObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Satellites_CelestialObjectId",
                table: "Satellites",
                column: "CelestialObjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Satellites");
        }
    }
}
