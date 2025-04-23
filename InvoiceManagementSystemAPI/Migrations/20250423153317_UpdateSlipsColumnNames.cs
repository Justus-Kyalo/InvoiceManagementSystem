using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceManagementSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSlipsColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "updatedDate",
                table: "Slips",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "createdDate",
                table: "Slips",
                newName: "CreatedDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Slips",
                newName: "updatedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Slips",
                newName: "createdDate");
        }
    }
}
