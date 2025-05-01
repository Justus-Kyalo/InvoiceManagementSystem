using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceManagementSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class NavigationBuilder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SlipNumber",
                table: "Slips",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ItemName",
                table: "Items",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Slips_SlipNumber",
                table: "Slips",
                column: "SlipNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemName",
                table: "Items",
                column: "ItemName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Slips_SlipNumber",
                table: "Slips");

            migrationBuilder.DropIndex(
                name: "IX_Items_ItemName",
                table: "Items");

            migrationBuilder.AlterColumn<string>(
                name: "SlipNumber",
                table: "Slips",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ItemName",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
