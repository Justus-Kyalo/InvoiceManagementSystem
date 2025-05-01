using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceManagementSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class AlterSlipItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SlipItem_Items_ItemId",
                table: "SlipItem");

            migrationBuilder.DropForeignKey(
                name: "FK_SlipItem_Slips_SlipId",
                table: "SlipItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SlipItem",
                table: "SlipItem");

            migrationBuilder.RenameTable(
                name: "SlipItem",
                newName: "SlipItems");

            migrationBuilder.RenameIndex(
                name: "IX_SlipItem_SlipId",
                table: "SlipItems",
                newName: "IX_SlipItems_SlipId");

            migrationBuilder.RenameIndex(
                name: "IX_SlipItem_ItemId",
                table: "SlipItems",
                newName: "IX_SlipItems_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SlipItems",
                table: "SlipItems",
                column: "SlipItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_SlipItems_Items_ItemId",
                table: "SlipItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SlipItems_Slips_SlipId",
                table: "SlipItems",
                column: "SlipId",
                principalTable: "Slips",
                principalColumn: "SlipId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SlipItems_Items_ItemId",
                table: "SlipItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SlipItems_Slips_SlipId",
                table: "SlipItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SlipItems",
                table: "SlipItems");

            migrationBuilder.RenameTable(
                name: "SlipItems",
                newName: "SlipItem");

            migrationBuilder.RenameIndex(
                name: "IX_SlipItems_SlipId",
                table: "SlipItem",
                newName: "IX_SlipItem_SlipId");

            migrationBuilder.RenameIndex(
                name: "IX_SlipItems_ItemId",
                table: "SlipItem",
                newName: "IX_SlipItem_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SlipItem",
                table: "SlipItem",
                column: "SlipItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_SlipItem_Items_ItemId",
                table: "SlipItem",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SlipItem_Slips_SlipId",
                table: "SlipItem",
                column: "SlipId",
                principalTable: "Slips",
                principalColumn: "SlipId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
