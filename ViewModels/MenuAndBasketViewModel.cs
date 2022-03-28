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

namespace WPF_Restaurant.ViewModels
{
    public class MenuAndBasketViewModel : BaseViewModel
    {
        private readonly ObservableCollection<DishViewModel> _dishesInMenu;

        private readonly ObservableCollection<DishViewModel> _chosenDishes;

        public IEnumerable<DishViewModel> DishesInMenu => _dishesInMenu;

        public IEnumerable<DishViewModel> ChosenDishes => _chosenDishes;

        public ICommand ChooseDishCommand { get; }

        public ICommand IncreaseQuantityCommand { get; }

        public ICommand DecreaseQuantityCommand { get; }

        public ICommand RemoveDishCommand { get; }

        public ICommand LoadDishesCommand { get; }


        public MenuAndBasketViewModel(Restaurant restaurant)
        {
            _dishesInMenu = new ObservableCollection<DishViewModel>();
            _chosenDishes = new ObservableCollection<DishViewModel>();
            ChooseDishCommand = new ChooseDishCommand(_chosenDishes);
            IncreaseQuantityCommand = new IncreaseQuantityCommand(_chosenDishes);
            DecreaseQuantityCommand = new DecreaseQuantityCommand(_chosenDishes);
            RemoveDishCommand = new RemoveDishCommand(_chosenDishes);
            LoadDishesCommand = new LoadDishesCommand(_dishesInMenu, restaurant);
        }

        public static MenuAndBasketViewModel LoadViewModel(Restaurant restaurant)
        {
            var viewModel = new MenuAndBasketViewModel(restaurant);
            viewModel.LoadDishesCommand.Execute(null);
            return viewModel;
        }
    }
}
