using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Data_ClassLib.Migrations
{
    public partial class tieuchangedatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "GioHang");

            migrationBuilder.DropColumn(
                name: "TotalMoney",
                table: "GioHang");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "GioHang");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "GioHangCT",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "GioHangCT",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "GioHangCT",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "GioHangCT",
                newName: "Id");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "GioHangCT",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "GioHangCT",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "GioHangCT",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "GioHangCT",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "GioHangCT",
                newName: "id");

            migrationBuilder.AlterColumn<int>(
                name: "price",
                table: "GioHangCT",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
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

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "GioHang",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
