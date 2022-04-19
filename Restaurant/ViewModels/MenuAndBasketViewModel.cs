﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_Restaurant.Commands;
using WPF_Restaurant.DataAccess.Data;
using WPF_Restaurant.Stores;

namespace WPF_Restaurant.ViewModels
{
    public class MenuAndBasketViewModel : BaseViewModel
    {
        private readonly ObservableCollection<DishViewModel> _dishesInMenu;

        private readonly ObservableCollection<DishViewModel> _chosenDishes;

        public ObservableCollection<DishViewModel> DishesInMenu => _dishesInMenu;

        public ObservableCollection<DishViewModel> ChosenDishes => _chosenDishes;

        public MessageViewModel MessageViewModel { get; }

        public ICommand ChooseDishCommand { get; }

        public ICommand IncreaseQuantityCommand { get; }

        public ICommand DecreaseQuantityCommand { get; }

        public ICommand RemoveDishCommand { get; }

        public ICommand LoadDishesCommand { get; }

        public ICommand OrderCommand { get; }

        public ICommand NavigateCommand { get; }


        public MenuAndBasketViewModel(
            Restaurant restaurant,
            NavigationStore navigationStore,
            Func<MainChefViewModel> mainChefViewModel,
            MessageStore messageStore,
            MessageViewModel messageViewModel,
            ILoggerFactory factory)
        {
            _dishesInMenu = new ObservableCollection<DishViewModel>();
            _chosenDishes = new ObservableCollection<DishViewModel>();
            ChooseDishCommand = new ChooseDishCommand(_chosenDishes, messageStore, factory);
            IncreaseQuantityCommand = new IncreaseQuantityCommand(_chosenDishes, messageStore, factory);
            DecreaseQuantityCommand = new DecreaseQuantityCommand(_chosenDishes, messageStore, factory);
            RemoveDishCommand = new RemoveDishCommand(_chosenDishes, messageStore, factory);
            LoadDishesCommand = new LoadDishesCommand(_dishesInMenu, restaurant, messageStore, factory);
            OrderCommand = new CreateOrderCommand(_chosenDishes, restaurant, messageStore, factory);
            NavigateCommand = new NavigateCommand(navigationStore, mainChefViewModel);
            MessageViewModel = messageViewModel;
        }

        public static MenuAndBasketViewModel LoadViewModel(
            Restaurant restaurant,
            NavigationStore navigationStore,
            Func<MainChefViewModel> mainChefViewModel,
            MessageStore messageStore,
            MessageViewModel messageViewModel,
            ILoggerFactory factory)
        {
            var viewModel = new MenuAndBasketViewModel(restaurant, navigationStore, mainChefViewModel, messageStore, messageViewModel, factory);
            viewModel.LoadDishesCommand.Execute(null);
            return viewModel;
        }
    }
}