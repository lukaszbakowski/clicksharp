using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClickSharp.DataLayer.Migrations
{
    public partial class anotherInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Attempts",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "IpAddr",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastAuth",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attempts",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IpAddr",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastAuth",
                table: "Users");
        }
    }
}
