using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_End_Capstone.Migrations
{
    /// <inheritdoc />
    public partial class sessionIdIsPagato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StripeSessionId",
                table: "Ordini",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPagato",
                table: "Ordini",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPagato",
                table: "Ordini");

            migrationBuilder.AlterColumn<string>(
                name: "StripeSessionId",
                table: "Ordini",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
