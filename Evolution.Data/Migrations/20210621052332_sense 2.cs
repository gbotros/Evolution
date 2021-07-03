using Microsoft.EntityFrameworkCore.Migrations;

namespace Evolution.Data.Migrations
{
    public partial class sense2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AnimalDefaults_SenseMutationAmplitude",
                table: "GameSettings",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnimalDefaults_SenseMutationAmplitude",
                table: "GameSettings");
        }
    }
}
