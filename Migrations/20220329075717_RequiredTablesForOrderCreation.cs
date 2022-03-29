using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPF_Restaurant.Migrations
{
    public partial class RequiredTablesForOrderCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "DishesInOrder",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DishesInOrder_OrderId",
                table: "DishesInOrder",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_DishesInOrder_Orders_OrderId",
                table: "DishesInOrder",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DishesInOrder_Orders_OrderId",
                table: "DishesInOrder");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_DishesInOrder_OrderId",
                table: "DishesInOrder");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "DishesInOrder");
        }
    }
}
