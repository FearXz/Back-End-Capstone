using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_End_Capstone.Migrations
{
    /// <inheritdoc />
    public partial class giornichiusura1am : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GiorniChiusuraRistoranti_IdGiorniChiusura",
                table: "GiorniChiusuraRistoranti");

            migrationBuilder.CreateIndex(
                name: "IX_GiorniChiusuraRistoranti_IdGiorniChiusura",
                table: "GiorniChiusuraRistoranti",
                column: "IdGiorniChiusura");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GiorniChiusuraRistoranti_IdGiorniChiusura",
                table: "GiorniChiusuraRistoranti");

            migrationBuilder.CreateIndex(
                name: "IX_GiorniChiusuraRistoranti_IdGiorniChiusura",
                table: "GiorniChiusuraRistoranti",
                column: "IdGiorniChiusura",
                unique: true);
        }
    }
}
