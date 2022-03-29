﻿using System;
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
        private readonly List<DishViewModel> _dishesInMenu;

        private readonly ObservableCollection<DishViewModel> _chosenDishes;

        public List<DishViewModel> DishesInMenu => _dishesInMenu;

        public ObservableCollection<DishViewModel> ChosenDishes => _chosenDishes;

        public ICommand ChooseDishCommand { get; }

        public ICommand IncreaseQuantityCommand { get; }

        public ICommand DecreaseQuantityCommand { get; }

        public ICommand RemoveDishCommand { get; }

        public ICommand LoadDishesCommand { get; }

        public ICommand OrderCommand { get; set; }


        public MenuAndBasketViewModel(Restaurant restaurant)
        {
            _dishesInMenu = new List<DishViewModel>();
            _chosenDishes = new ObservableCollection<DishViewModel>();
            ChooseDishCommand = new ChooseDishCommand(_chosenDishes);
            IncreaseQuantityCommand = new IncreaseQuantityCommand(_chosenDishes);
            DecreaseQuantityCommand = new DecreaseQuantityCommand(_chosenDishes);
            RemoveDishCommand = new RemoveDishCommand(_chosenDishes);
            LoadDishesCommand = new LoadDishesCommand(_dishesInMenu, restaurant);
            OrderCommand = new CreateOrderCommand(_chosenDishes, restaurant);
        }

        public static MenuAndBasketViewModel LoadViewModel(Restaurant restaurant)
        {
            var viewModel = new MenuAndBasketViewModel(restaurant);
            viewModel.LoadDishesCommand.Execute(null);
            return viewModel;
        }
    }
}
