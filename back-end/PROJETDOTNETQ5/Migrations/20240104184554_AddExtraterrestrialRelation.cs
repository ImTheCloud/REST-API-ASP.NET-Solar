using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROJETDOTNETQ5.Migrations
{
    /// <inheritdoc />
    public partial class AddExtraterrestrialRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.CreateTable(
                name: "CelestialObjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Mesh = table.Column<double>(type: "float", nullable: false),
                    Obj = table.Column<double>(type: "float", nullable: false),
                    UrlImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PositionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BestMomentsAndSpots = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CelestialObjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Extraterrestrials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BestMomentsAndSpots = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CelestialObjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extraterrestrials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Extraterrestrials_CelestialObjects_CelestialObjectId",
                        column: x => x.CelestialObjectId,
                        principalTable: "CelestialObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Extraterrestrials_CelestialObjectId",
                table: "Extraterrestrials",
                column: "CelestialObjectId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Extraterrestrials");

            migrationBuilder.DropTable(
                name: "CelestialObjects");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailAddress = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }
    }
}
