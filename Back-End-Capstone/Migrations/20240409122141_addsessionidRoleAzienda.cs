using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_End_Capstone.Migrations
{
    /// <inheritdoc />
    public partial class addsessionidRoleAzienda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StripeSessionId",
                table: "Ordini",
                type: "nvarchar(max)",
                nullable: true
            );

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Aziende",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: ""
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "StripeSessionId", table: "Ordini");

            migrationBuilder.DropColumn(name: "Role", table: "Aziende");
        }
    }
}
