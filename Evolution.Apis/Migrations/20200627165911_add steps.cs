using Microsoft.EntityFrameworkCore.Migrations;

namespace Evolution.Apis.Migrations
{
    public partial class addsteps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Steps",
                table: "Animals",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Steps",
                table: "Animals");
        }
    }
}
