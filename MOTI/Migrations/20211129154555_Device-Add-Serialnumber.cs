using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOTI.Migrations
{
    public partial class DeviceAddSerialnumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "Devices",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "Devices");
        }
    }
}
