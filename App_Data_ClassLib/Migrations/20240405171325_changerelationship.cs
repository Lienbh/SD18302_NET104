using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Data_ClassLib.Migrations
{
    public partial class changerelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_GioHang_ID",
                table: "User");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "GioHang",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_GioHang_UserId",
                table: "GioHang",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GioHang_User_UserId",
                table: "GioHang",
                column: "UserId",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GioHang_User_UserId",
                table: "GioHang");

            migrationBuilder.DropIndex(
                name: "IX_GioHang_UserId",
                table: "GioHang");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GioHang");

            migrationBuilder.AddForeignKey(
                name: "FK_User_GioHang_ID",
                table: "User",
                column: "ID",
                principalTable: "GioHang",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
