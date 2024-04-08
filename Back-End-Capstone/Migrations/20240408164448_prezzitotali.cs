using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_End_Capstone.Migrations
{
    /// <inheritdoc />
    public partial class prezzitotali : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PrezzoTotale",
                table: "ProdottiAcquistati",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotaleOrdine",
                table: "Ordini",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrezzoTotale",
                table: "ProdottiAcquistati");

            migrationBuilder.DropColumn(
                name: "TotaleOrdine",
                table: "Ordini");
        }
    }
}
