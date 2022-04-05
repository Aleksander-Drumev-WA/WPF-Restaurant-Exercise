using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Restaurant.ViewModels
{
    public class ChefLookingAtRecipeViewModel : BaseViewModel
    {
        private readonly int _orderNumber;
        private readonly string _dishName;
        private readonly string _dishRecipe;

        public int OrderNumber => _orderNumber;

        public string DishName => _dishName;

        public string DishRecipe => _dishRecipe;

        public ChefLookingAtRecipeViewModel(int orderNumber, string dishName, string dishRecipe)
        {
            _orderNumber = orderNumber;
            _dishName = dishName;
            _dishRecipe = dishRecipe;
        }
    }
}
