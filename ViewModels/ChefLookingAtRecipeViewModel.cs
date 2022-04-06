using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_Restaurant.Commands;
using WPF_Restaurant.Models;

namespace WPF_Restaurant.ViewModels
{
    public class ChefLookingAtRecipeViewModel : BaseViewModel
    {
        private readonly int _orderNumber;
        private readonly string _dishName;
        private readonly string _dishRecipe;
        private readonly int _dishId;

        public int DishId => _dishId;

        public int OrderNumber => _orderNumber;

        public string DishName => _dishName;

        public string DishRecipe => _dishRecipe;

        public ICommand CompleteDishesCommand { get; }

        public ICommand LoadOrdersCommand { get; }

        public ChefLookingAtRecipeViewModel(int orderNumber, int dishId, string dishName, string dishRecipe, Restaurant restaurant, ObservableCollection<OrderViewModel> orders)
        {
            _orderNumber = orderNumber;
            _dishName = dishName;
            _dishRecipe = dishRecipe;
            _dishId = dishId;
            LoadOrdersCommand = new LoadOrdersCommand(orders, restaurant);
            CompleteDishesCommand = new CompleteDishesCommand(restaurant, LoadOrdersCommand);
            LoadOrdersCommand = new LoadOrdersCommand(orders, restaurant);
        }
    }
}
