using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaWebsite.Migrations
{
    public partial class AddBackgroundImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BackGroundImageLocation",
                table: "Pizzas",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackGroundImageLocation",
                table: "Pizzas");
        }
    }
}
