using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOTI.Migrations
{
    public partial class AddedConsumption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Consumption",
                table: "Devices",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Consumption",
                table: "Devices");
        }
    }
}
