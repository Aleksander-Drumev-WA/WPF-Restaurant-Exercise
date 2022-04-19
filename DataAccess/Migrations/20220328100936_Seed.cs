using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPF_Restaurant.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var dir = Environment.CurrentDirectory.Replace("bin\\Debug\\net6.0-windows", "Resources\\Images\\");
            migrationBuilder.Sql($"INSERT INTO Dishes (Name, ImagePath, Recipe, Ingredients) VALUES ('Pizza', '{dir}pizza.jpg', 'Whatever recipe', 'Pepperoni, Mushroom, Onion, Olives, Mozzarella');");
            migrationBuilder.Sql($"INSERT INTO Dishes (Name, ImagePath, Recipe, Ingredients) VALUES ('Burger', '{dir}burger.jpg', 'Whatever recipe', 'Beef, Egg, Onion, Mayonnaise, Iceberg lettuce leaves, Tomato');");
            migrationBuilder.Sql($"INSERT INTO Dishes (Name, ImagePath, Recipe, Ingredients) VALUES ('Beef', '{dir}beef.jpg', 'Whatever recipe', 'Beef');");
            migrationBuilder.Sql($"INSERT INTO Dishes (Name, ImagePath, Recipe, Ingredients) VALUES ('Beer', '{dir}beer.jpg', 'Whatever recipe', 'Beer');");
            migrationBuilder.Sql($"INSERT INTO Dishes (Name, ImagePath, Recipe, Ingredients) VALUES ('Ice Cream', '{dir}ice-cream.jpg', 'Whatever recipe', 'Chocolate, Vanillia, Oreo');");
            migrationBuilder.Sql($"INSERT INTO Dishes (Name, ImagePath, Recipe, Ingredients) VALUES ('Pasta', '{dir}pasta.jpg', 'Whatever recipe', 'Pasta, Onion, Garlic, Carrots, Sweet paprika, Parsley');");
            migrationBuilder.Sql($"INSERT INTO Dishes (Name, ImagePath, Recipe, Ingredients) VALUES ('Garlic Potatoes', '{dir}potatoes.jpg', 'Whatever recipe', 'Potatoes, Garlic');");
            migrationBuilder.Sql($"INSERT INTO Dishes (Name, ImagePath, Recipe, Ingredients) VALUES ('Rice', '{dir}rice.jpg', 'Whatever recipe', 'Carrots, Chicken breast, Cinnamon stick, Lemon zest');");
            migrationBuilder.Sql($"INSERT INTO Dishes (Name, ImagePath, Recipe, Ingredients) VALUES ('Salad', '{dir}salad.jpg', 'Whatever recipe', 'Tomatoes, Cucumbers, Onion, Corn');");
            migrationBuilder.Sql($"INSERT INTO Dishes (Name, ImagePath, Recipe, Ingredients) VALUES ('Chocolate Cake', '{dir}triple-chocolate-cake.jpg', 'Whatever recipe', 'Chocolate, Sugar, Milk');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
