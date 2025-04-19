using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceManagementSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateSlipsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "Slips",
                columns: table => new
                {
                    SlipId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SlipDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehicleRegistration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SlipNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slips", x => x.SlipId);
                });

            migrationBuilder.CreateTable(
                name: "SlipItem",
                columns: table => new
                {
                    SlipItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SlipId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlipItem", x => x.SlipItemId);
                    table.ForeignKey(
                        name: "FK_SlipItem_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SlipItem_Slips_SlipId",
                        column: x => x.SlipId,
                        principalTable: "Slips",
                        principalColumn: "SlipId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SlipItem_ItemId",
                table: "SlipItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SlipItem_SlipId",
                table: "SlipItem",
                column: "SlipId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SlipItem");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Slips");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerItem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Taxable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectionSlipNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Item = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VehicleRegistration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                });
        }
    }
}
