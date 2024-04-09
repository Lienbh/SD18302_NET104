using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Data_ClassLib.Migrations
{
    public partial class themthongtingiohang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "GioHang",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "GioHang",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "GioHang",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "GioHang",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalMoney",
                table: "GioHang",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "GioHang");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "GioHang");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "GioHang");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "GioHang");

            migrationBuilder.DropColumn(
                name: "TotalMoney",
                table: "GioHang");
        }
    }
}
