using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Restaurant.Models
{
    public class Dish
    {
        public int Id { get; }

        public string Name { get; }

        public string ImagePath { get; }

        public string Recipe { get; }

        public string Ingredients { get; }

        public int Quantity { get; set; }

        public bool IsCompleted { get; set; }

        public Dish(int id, string name, string imagePath, string recipe, params string[] ingredients)
        {
            Name = name;
            ImagePath = imagePath;
            Recipe = recipe;
            Id = id;
            Quantity = 1;
            Ingredients = string.Join(", ", ingredients);
        }

        public Dish(int id, string name, string imagePath, string recipe, bool isCompleted, params string[] ingredients) 
            : this(id, name, imagePath, recipe, ingredients)
        {
            IsCompleted = isCompleted;
        }
    }
}
