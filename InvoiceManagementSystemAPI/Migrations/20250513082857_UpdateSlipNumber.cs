using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceManagementSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSlipNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SlipNumber",
                table: "Slips",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SlipNumber",
                table: "Slips",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
