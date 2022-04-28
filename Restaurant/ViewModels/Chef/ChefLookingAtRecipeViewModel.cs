using Microsoft.Extensions.Logging;
using System.Windows.Input;

using WPF_Restaurant.Commands;
using WPF_Restaurant.DataAccess.Data;
using WPF_Restaurant.Stores;
using WPF_Restaurant.ViewModels.Common;

namespace WPF_Restaurant.ViewModels.Chef
{
    public class ChefLookingAtRecipeViewModel : BaseViewModel
    {
        private readonly OrderItemViewModel _chosenDish;
        private readonly int _dishId;

        public int DishId => _dishId;

        public int OrderNumber => _chosenDish.OrderNumber;

        public string DishName => _chosenDish.Name;

        public string DishRecipe => _chosenDish.Recipe;

        public ICommand CompleteDishCommand { get; }

        public ICommand LoadOrdersCommand { get; }

		public ChefLookingAtRecipeViewModel(
            OrderItemViewModel chosenDish,
            int id,
            Restaurant restaurant,
            MainChefViewModel mainChefViewModel,
            MessageStore messageStore,
            ILoggerFactory factory)
		{
            _chosenDish = chosenDish;
            _dishId = id;
            LoadOrdersCommand = new LoadOrdersCommand(mainChefViewModel.Orders, restaurant, messageStore, factory);
            CompleteDishCommand = new CompleteDishCommand(restaurant, LoadOrdersCommand, messageStore, factory, mainChefViewModel);
        }
	}
}
