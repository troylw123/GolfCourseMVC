using Microsoft.EntityFrameworkCore.Migrations;

namespace GolfCourseMVC.Server.Data.Migrations
{
    public partial class FixedTeeTimeEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Time",
                table: "Prices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Prices");
        }
    }
}
