using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceManagementSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddVehicleId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehicleRegistration",
                table: "Slips");

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Slips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Slips_VehicleId",
                table: "Slips",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Slips_Vehicles_VehicleId",
                table: "Slips",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slips_Vehicles_VehicleId",
                table: "Slips");

            migrationBuilder.DropIndex(
                name: "IX_Slips_VehicleId",
                table: "Slips");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Slips");

            migrationBuilder.AddColumn<string>(
                name: "VehicleRegistration",
                table: "Slips",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
