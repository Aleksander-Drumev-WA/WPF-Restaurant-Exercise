using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_Restaurant.Commands;
using WPF_Restaurant.Models;
using WPF_Restaurant.Stores;

namespace WPF_Restaurant.ViewModels
{
    public class MenuAndBasketViewModel : BaseViewModel
    {
        private readonly ObservableCollection<DishViewModel> _dishesInMenu;

        private readonly ObservableCollection<DishViewModel> _chosenDishes;

        public ObservableCollection<DishViewModel> DishesInMenu => _dishesInMenu;

        public ObservableCollection<DishViewModel> ChosenDishes => _chosenDishes;

        public ICommand ChooseDishCommand { get; }

        public ICommand IncreaseQuantityCommand { get; }

        public ICommand DecreaseQuantityCommand { get; }

        public ICommand RemoveDishCommand { get; }

        public ICommand LoadDishesCommand { get; }

        public ICommand OrderCommand { get; }

        public ICommand NavigateCommand { get; }


        public MenuAndBasketViewModel(Restaurant restaurant, NavigationStore navigationStore, Func<MainChefViewModel> mainChefViewModel)
        {
            _dishesInMenu = new ObservableCollection<DishViewModel>();
            _chosenDishes = new ObservableCollection<DishViewModel>();
            ChooseDishCommand = new ChooseDishCommand(_chosenDishes);
            IncreaseQuantityCommand = new IncreaseQuantityCommand(_chosenDishes);
            DecreaseQuantityCommand = new DecreaseQuantityCommand(_chosenDishes);
            RemoveDishCommand = new RemoveDishCommand(_chosenDishes);
            LoadDishesCommand = new LoadDishesCommand(_dishesInMenu, restaurant);
            OrderCommand = new CreateOrderCommand(_chosenDishes, restaurant);
            NavigateCommand = new NavigateCommand(navigationStore, mainChefViewModel);
        }

        public static MenuAndBasketViewModel LoadViewModel(Restaurant restaurant, NavigationStore navigationStore, Func<MainChefViewModel> mainChefViewModel)
        {
            var viewModel = new MenuAndBasketViewModel(restaurant, navigationStore, mainChefViewModel);
            viewModel.LoadDishesCommand.Execute(null);
            return viewModel;
        }
    }
}
