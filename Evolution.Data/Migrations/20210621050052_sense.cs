using Microsoft.EntityFrameworkCore.Migrations;

namespace Evolution.Data.Migrations
{
    public partial class sense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnimalDefaults_MaxSense",
                table: "GameSettings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnimalDefaults_MinSense",
                table: "GameSettings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxSense",
                table: "Animals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinSense",
                table: "Animals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "SenseMutationAmplitude",
                table: "Animals",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnimalDefaults_MaxSense",
                table: "GameSettings");

            migrationBuilder.DropColumn(
                name: "AnimalDefaults_MinSense",
                table: "GameSettings");

            migrationBuilder.DropColumn(
                name: "MaxSense",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "MinSense",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "SenseMutationAmplitude",
                table: "Animals");
        }
    }
}
