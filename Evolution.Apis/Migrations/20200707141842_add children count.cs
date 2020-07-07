using Microsoft.EntityFrameworkCore.Migrations;

namespace Evolution.Apis.Migrations
{
    public partial class addchildrencount : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "ChildrenCount",
                "Animals");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "ChildrenCount",
                "Animals",
                nullable: false,
                defaultValue: 0);
        }
    }
}