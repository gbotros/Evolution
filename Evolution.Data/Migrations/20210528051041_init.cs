using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Evolution.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Steps = table.Column<int>(type: "int", nullable: false),
                    StoredFood = table.Column<int>(type: "int", nullable: false),
                    FoodStorageCapacity = table.Column<int>(type: "int", nullable: false),
                    Energy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
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
                    Column = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "Plants");
        }
    }
}
