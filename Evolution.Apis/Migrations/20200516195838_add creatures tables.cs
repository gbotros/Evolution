using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Evolution.Apis.Migrations
{
    public partial class addcreaturestables : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Animals");

            migrationBuilder.DropTable(
                "Plants");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Animals",
                table => new
                {
                    Id = table.Column<Guid>(),
                    BirthDay = table.Column<int>(),
                    DeathDay = table.Column<int>(nullable: true),
                    Energy = table.Column<int>(),
                    IsAlive = table.Column<bool>(),
                    Location_X = table.Column<int>(nullable: true),
                    Location_Y = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Speed = table.Column<int>(),
                    Weight = table.Column<int>()
                },
                constraints: table => { table.PrimaryKey("PK_Animals", x => x.Id); });

            migrationBuilder.CreateTable(
                "Plants",
                table => new
                {
                    Id = table.Column<Guid>(),
                    BirthDay = table.Column<int>(),
                    DeathDay = table.Column<int>(nullable: true),
                    GrowthAmount = table.Column<int>(),
                    IsAlive = table.Column<bool>(),
                    Location_X = table.Column<int>(nullable: true),
                    Location_Y = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Weight = table.Column<int>()
                },
                constraints: table => { table.PrimaryKey("PK_Plants", x => x.Id); });
        }
    }
}