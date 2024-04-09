using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Data_ClassLib.Migrations
{
    public partial class addSatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "GioHang",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "GioHang");
        }
    }
}
