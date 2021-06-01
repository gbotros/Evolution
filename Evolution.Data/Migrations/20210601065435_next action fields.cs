using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Evolution.Data.Migrations
{
    public partial class nextactionfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastAction",
                table: "Animals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NextAction",
                table: "Animals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastAction",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "NextAction",
                table: "Animals");
        }
    }
}
