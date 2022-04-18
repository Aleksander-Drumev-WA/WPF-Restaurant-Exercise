using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPF_Restaurant.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Dishes (Name, ImagePath, Recipe, Ingredients) VALUES ('Pizza', 'E:\\WPF-Restaurant\\Resources\\Images\\pizza.jpg', 'Whatever recipe', 'Pepperoni, Mushroom, Onion, Olives, Mozzarella');");
            migrationBuilder.Sql("INSERT INTO Dishes (Name, ImagePath, Recipe, Ingredients) VALUES ('Burger', 'E:\\WPF-Restaurant\\Resources\\Images\\burger.jpg', 'Whatever recipe', 'Beef, Egg, Onion, Mayonnaise, Iceberg lettuce leaves, Tomato');");
            migrationBuilder.Sql("INSERT INTO Dishes (Name, ImagePath, Recipe, Ingredients) VALUES ('Beef', 'E:\\WPF-Restaurant\\Resources\\Images\\beef.jpg', 'Whatever recipe', 'Beef');");
            migrationBuilder.Sql("INSERT INTO Dishes (Name, ImagePath, Recipe, Ingredients) VALUES ('Beer', 'E:\\WPF-Restaurant\\Resources\\Images\\beer.jpg', 'Whatever recipe', 'Beer');");
            migrationBuilder.Sql("INSERT INTO Dishes (Name, ImagePath, Recipe, Ingredients) VALUES ('Ice Cream', 'E:\\WPF-Restaurant\\Resources\\Images\\ice-cream.jpg', 'Whatever recipe', 'Chocolate, Vanillia, Oreo');");
            migrationBuilder.Sql("INSERT INTO Dishes (Name, ImagePath, Recipe, Ingredients) VALUES ('Pasta', 'E:\\WPF-Restaurant\\Resources\\Images\\pasta.jpg', 'Whatever recipe', 'Pasta, Onion, Garlic, Carrots, Sweet paprika, Parsley');");
            migrationBuilder.Sql("INSERT INTO Dishes (Name, ImagePath, Recipe, Ingredients) VALUES ('Garlic Potatoes', 'E:\\WPF-Restaurant\\Resources\\Images\\potatoes.jpg', 'Whatever recipe', 'Potatoes, Garlic');");
            migrationBuilder.Sql("INSERT INTO Dishes (Name, ImagePath, Recipe, Ingredients) VALUES ('Rice', 'E:\\WPF-Restaurant\\Resources\\Images\\rice.jpg', 'Whatever recipe', 'Carrots, Chicken breast, Cinnamon stick, Lemon zest');");
            migrationBuilder.Sql("INSERT INTO Dishes (Name, ImagePath, Recipe, Ingredients) VALUES ('Salad', 'E:\\WPF-Restaurant\\Resources\\Images\\salad.jpg', 'Whatever recipe', 'Tomatoes, Cucumbers, Onion, Corn');");
            migrationBuilder.Sql("INSERT INTO Dishes (Name, ImagePath, Recipe, Ingredients) VALUES ('Chocolate Cake', 'E:\\WPF-Restaurant\\Resources\\Images\\triple-chocolate-cake.jpg', 'Whatever recipe', 'Chocolate, Sugar, Milk');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
