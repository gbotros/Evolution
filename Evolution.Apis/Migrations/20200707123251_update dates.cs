using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Evolution.Apis.Migrations
{
    public partial class updatedates : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "BirthDate",
                "Plants");

            migrationBuilder.DropColumn(
                "DeathDate",
                "Plants");

            migrationBuilder.DropColumn(
                "ParentId",
                "Plants");

            migrationBuilder.DropColumn(
                "UpdatedAt",
                "Plants");

            migrationBuilder.DropColumn(
                "BirthDate",
                "Animals");

            migrationBuilder.DropColumn(
                "DeathDate",
                "Animals");

            migrationBuilder.DropColumn(
                "ParentId",
                "Animals");

            migrationBuilder.DropColumn(
                "UpdatedAt",
                "Animals");

            migrationBuilder.AddColumn<int>(
                "BirthDay",
                "Plants",
                "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "DeathDay",
                "Plants",
                "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "BirthDay",
                "Animals",
                "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "DeathDay",
                "Animals",
                "int",
                nullable: true);
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "BirthDay",
                "Plants");

            migrationBuilder.DropColumn(
                "DeathDay",
                "Plants");

            migrationBuilder.DropColumn(
                "BirthDay",
                "Animals");

            migrationBuilder.DropColumn(
                "DeathDay",
                "Animals");

            migrationBuilder.AddColumn<DateTime>(
                "BirthDate",
                "Plants",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "DeathDate",
                "Plants",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                "ParentId",
                "Plants",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                "UpdatedAt",
                "Plants",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "BirthDate",
                "Animals",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "DeathDate",
                "Animals",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                "ParentId",
                "Animals",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                "UpdatedAt",
                "Animals",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}