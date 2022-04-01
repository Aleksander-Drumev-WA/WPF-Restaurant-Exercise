using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Restaurant.Models
{
    public class Dish
    {
        public string Name { get; }

        public string ImagePath { get; }

        public string Recipe { get; }

        public string Ingredients { get; }

        public int Quantity { get; set; }

        public Dish(string name, string imagePath, string recipe, params string[] ingredients)
        {
            Name = name;
            ImagePath = imagePath;
            Recipe = recipe;
            Quantity = 1;
            Ingredients = string.Join(", ", ingredients);
        }
    }
}
