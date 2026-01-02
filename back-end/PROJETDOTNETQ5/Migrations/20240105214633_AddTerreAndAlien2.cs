using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROJETDOTNETQ5.Migrations
{
    /// <inheritdoc />
    public partial class AddTerreAndAlien2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alien_CelestialObjects_CelestialObjectId",
                table: "Alien");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Alien",
                table: "Alien");

            migrationBuilder.RenameTable(
                name: "Alien",
                newName: "Aliens");

            migrationBuilder.RenameIndex(
                name: "IX_Alien_CelestialObjectId",
                table: "Aliens",
                newName: "IX_Aliens_CelestialObjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Aliens",
                table: "Aliens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Aliens_CelestialObjects_CelestialObjectId",
                table: "Aliens",
                column: "CelestialObjectId",
                principalTable: "CelestialObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aliens_CelestialObjects_CelestialObjectId",
                table: "Aliens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Aliens",
                table: "Aliens");

            migrationBuilder.RenameTable(
                name: "Aliens",
                newName: "Alien");

            migrationBuilder.RenameIndex(
                name: "IX_Aliens_CelestialObjectId",
                table: "Alien",
                newName: "IX_Alien_CelestialObjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Alien",
                table: "Alien",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Alien_CelestialObjects_CelestialObjectId",
                table: "Alien",
                column: "CelestialObjectId",
                principalTable: "CelestialObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
