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

        public BaseViewModel CurrentViewModel
        {
            get => _baseViewModel;
            set
            {
                _baseViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public MainChefViewModel(NavigationStore navigationStore, Func<MenuAndBasketViewModel> createMenuAndBasketViewModel, Restaurant restaurant)
        {
            _orders = new ObservableCollection<OrderViewModel>();
            NavigateCommand = new NavigateCommand(navigationStore, createMenuAndBasketViewModel);
            LoadOrdersCommand = new LoadOrdersCommand(_orders, restaurant);
            NavigateToRecipeViewCommand = new ShowRecipeCommand(this, restaurant);
        }

        public static MainChefViewModel LoadViewModel(NavigationStore navigationStore, Func<MenuAndBasketViewModel> mainChefViewModel, Restaurant restaurant)
        {
            var viewModel = new MainChefViewModel(navigationStore, mainChefViewModel, restaurant);
            viewModel.LoadOrdersCommand.Execute(null);
            return viewModel;
        }
    }
}
