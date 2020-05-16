using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Evolution.Apis.Migrations
{
    public partial class addcreaturestables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BirthDay = table.Column<int>(nullable: false),
                    DeathDay = table.Column<int>(nullable: true),
                    Energy = table.Column<int>(nullable: false),
                    IsAlive = table.Column<bool>(nullable: false),
                    Location_X = table.Column<int>(nullable: true),
                    Location_Y = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Speed = table.Column<int>(nullable: false),
                    Weight = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BirthDay = table.Column<int>(nullable: false),
                    DeathDay = table.Column<int>(nullable: true),
                    GrowthAmount = table.Column<int>(nullable: false),
                    IsAlive = table.Column<bool>(nullable: false),
                    Location_X = table.Column<int>(nullable: true),
                    Location_Y = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Weight = table.Column<int>(nullable: false)
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
