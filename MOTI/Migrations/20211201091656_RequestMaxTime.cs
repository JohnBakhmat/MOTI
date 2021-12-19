using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOTI.Migrations
{
    public partial class RequestMaxTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MaxTime",
                table: "Requests",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxTime",
                table: "Requests");
        }
    }
}
