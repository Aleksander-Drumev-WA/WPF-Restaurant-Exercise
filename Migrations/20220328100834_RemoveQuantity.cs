using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPF_Restaurant.Migrations
{
    public partial class RemoveQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Dishes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Dishes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
