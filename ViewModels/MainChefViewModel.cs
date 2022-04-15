using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_Restaurant.Commands;
using WPF_Restaurant.Models;
using WPF_Restaurant.Stores;

namespace WPF_Restaurant.ViewModels
{
    public class MainChefViewModel : BaseViewModel
    {
        private readonly ObservableCollection<OrderViewModel> _orders;
        private BaseViewModel _baseViewModel;

        public ObservableCollection<OrderViewModel> Orders => _orders;

        public ICommand NavigateCommand { get; }

        public ICommand LoadOrdersCommand { get; }

        public ICommand NavigateToRecipeViewCommand { get; }

        public ICommand ShowDishesInOrderCommand { get; }

        public BaseViewModel CurrentViewModel
        {
            get => _baseViewModel;
            set
            {
                _baseViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public MessageViewModel MessageViewModel { get; }

        public MainChefViewModel(
            NavigationStore navigationStore,
            Func<MenuAndBasketViewModel> createMenuAndBasketViewModel,
            Restaurant restaurant,
            MessageStore messageStore,
            MessageViewModel messageViewModel, 
            ILoggerFactory factory)
        {
            _orders = new ObservableCollection<OrderViewModel>();
            NavigateCommand = new NavigateCommand(navigationStore, createMenuAndBasketViewModel);
            LoadOrdersCommand = new LoadOrdersCommand(_orders, restaurant, messageStore);
            NavigateToRecipeViewCommand = new ShowRecipeCommand(this, restaurant, messageStore, factory);
            ShowDishesInOrderCommand = new ShowDishesInOrderCommand(this, restaurant, messageStore, factory);
            MessageViewModel = messageViewModel;
        }

        public static MainChefViewModel LoadViewModel(
            NavigationStore navigationStore,
            Func<MenuAndBasketViewModel> mainChefViewModel,
            Restaurant restaurant,
            MessageStore messageStore,
            MessageViewModel messageViewModel, 
            ILoggerFactory factory)
        {
            var viewModel = new MainChefViewModel(navigationStore, mainChefViewModel, restaurant, messageStore, messageViewModel, factory);
            viewModel.LoadOrdersCommand.Execute(null);
            return viewModel;
        }
    }
}
