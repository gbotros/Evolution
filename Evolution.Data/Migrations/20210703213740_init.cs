using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Evolution.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorldSize_Width = table.Column<int>(type: "int", nullable: true),
                    WorldSize_Height = table.Column<int>(type: "int", nullable: true),
                    AnimalDefaults_MinSpeed = table.Column<double>(type: "float", nullable: true),
                    AnimalDefaults_MaxSpeed = table.Column<double>(type: "float", nullable: true),
                    AnimalDefaults_Speed = table.Column<double>(type: "float", nullable: true),
                    AnimalDefaults_MinEnergy = table.Column<double>(type: "float", nullable: true),
                    AnimalDefaults_MaxEnergy = table.Column<double>(type: "float", nullable: true),
                    AnimalDefaults_Energy = table.Column<double>(type: "float", nullable: true),
                    AnimalDefaults_MinFoodStorageCapacity = table.Column<int>(type: "int", nullable: true),
                    AnimalDefaults_MaxFoodStorageCapacity = table.Column<int>(type: "int", nullable: true),
                    AnimalDefaults_FoodStorageCapacity = table.Column<int>(type: "int", nullable: true),
                    AnimalDefaults_OneFoodToEnergy = table.Column<int>(type: "int", nullable: true),
                    AnimalDefaults_SpeedMutationAmplitude = table.Column<long>(type: "bigint", nullable: true),
                    AnimalDefaults_AdulthoodAge = table.Column<int>(type: "int", nullable: true),
                    AnimalDefaults_Sense = table.Column<int>(type: "int", nullable: true),
                    AnimalDefaults_MinSense = table.Column<int>(type: "int", nullable: true),
                    AnimalDefaults_MaxSense = table.Column<int>(type: "int", nullable: true),
                    AnimalDefaults_SenseMutationAmplitude = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeathTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAlive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Row = table.Column<int>(type: "int", nullable: true),
                    Column = table.Column<int>(type: "int", nullable: true),
                    ChildrenCount = table.Column<int>(type: "int", nullable: false),
                    Speed = table.Column<double>(type: "float", nullable: false),
                    MinSpeed = table.Column<double>(type: "float", nullable: false),
                    MaxSpeed = table.Column<double>(type: "float", nullable: false),
                    Steps = table.Column<int>(type: "int", nullable: false),
                    StoredFood = table.Column<int>(type: "int", nullable: false),
                    MinFoodStorageCapacity = table.Column<int>(type: "int", nullable: false),
                    MaxFoodStorageCapacity = table.Column<int>(type: "int", nullable: false),
                    FoodStorageCapacity = table.Column<int>(type: "int", nullable: false),
                    Energy = table.Column<double>(type: "float", nullable: false),
                    MinEnergy = table.Column<double>(type: "float", nullable: false),
                    MaxEnergy = table.Column<double>(type: "float", nullable: false),
                    LastAction = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextAction = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAdult = table.Column<bool>(type: "bit", nullable: false),
                    OneFoodToEnergy = table.Column<int>(type: "int", nullable: false),
                    SpeedMutationAmplitude = table.Column<long>(type: "bigint", nullable: false),
                    SenseMutationAmplitude = table.Column<long>(type: "bigint", nullable: false),
                    AdulthoodAge = table.Column<int>(type: "int", nullable: false),
                    LastChildAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sense = table.Column<int>(type: "int", nullable: false),
                    MinSense = table.Column<int>(type: "int", nullable: false),
                    MaxSense = table.Column<int>(type: "int", nullable: false),
                    Direction = table.Column<int>(type: "int", nullable: false),
                    SettingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Animals_GameSettings_SettingsId",
                        column: x => x.SettingsId,
                        principalTable: "GameSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeathTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAlive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Row = table.Column<int>(type: "int", nullable: true),
                    Column = table.Column<int>(type: "int", nullable: true),
                    GrowthAmount = table.Column<int>(type: "int", nullable: false),
                    SettingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plants_GameSettings_SettingsId",
                        column: x => x.SettingsId,
                        principalTable: "GameSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_SettingsId",
                table: "Animals",
                column: "SettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_SettingsId",
                table: "Plants",
                column: "SettingsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.DropTable(
                name: "GameSettings");
        }
    }
}
