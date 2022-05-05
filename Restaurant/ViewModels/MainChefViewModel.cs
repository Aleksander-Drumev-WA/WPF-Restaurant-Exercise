using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_Restaurant.Commands;
using WPF_Restaurant.DataAccess.Data;
using WPF_Restaurant.Stores;
using WPF_Restaurant.ViewModels.Chef;
using WPF_Restaurant.ViewModels.Common;

namespace WPF_Restaurant.ViewModels
{
    public class MainChefViewModel : BaseViewModel
    {
        private readonly ObservableCollection<OrderViewModel> _orders;
        private BaseViewModel _baseViewModel;
        private bool _notReadyCheck;
        private string _nameFilter;

        public ObservableCollection<OrderViewModel> Orders => _orders;

        public ICommand NavigateCommand { get; }

        public ICommand LoadOrdersCommand { get; }

        public ICommand NavigateToRecipeViewCommand { get; }

        public ICommand ShowDishesInOrderCommand { get; }

		public bool NotReadyFilterChecked 
        {
            get => _notReadyCheck;
			set
			{
                _notReadyCheck = value;
                OnPropertyChanged(nameof(NotReadyFilterChecked));
			}
        }

		public string NameFilter 
        {
            get => _nameFilter;
			set
			{
                _nameFilter = value;
                OnPropertyChanged(nameof(NameFilter));
			}
        }

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
            ICommand navigateCommand,
            Restaurant restaurant,
            MessageStore messageStore,
            MessageViewModel messageViewModel, 
            ILoggerFactory factory)
        {
            _orders = new ObservableCollection<OrderViewModel>();
            NavigateCommand = navigateCommand;
            LoadOrdersCommand = new LoadOrdersCommand(_orders, restaurant, messageStore, factory);
            LoadOrdersCommand.Execute(this);
            NavigateToRecipeViewCommand = new ShowRecipeCommand(this, restaurant, messageStore, factory);
            ShowDishesInOrderCommand = new ShowDishesInOrderCommand(this, restaurant, messageStore, factory);
            MessageViewModel = messageViewModel;
        }
    }
}
