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
using WPF_Restaurant.Extensions;
using WPF_Restaurant.Stores;
using WPF_Restaurant.ViewModels.Chef;
using WPF_Restaurant.ViewModels.Common;
using static WPF_Restaurant.Stores.MessageStore;

namespace WPF_Restaurant.ViewModels
{
    public class MainChefViewModel : BaseViewModel
    {
        private readonly ObservableCollection<OrderViewModel> _orders;
        private BaseViewModel _baseViewModel;
        private bool _notReadyCheck;
        private string _nameFilter;

        private Restaurant _restaurant;

        private ILoggerFactory _factory;
        private ILogger _logger;
        private IMessageStore _messageStore;

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
            //NavigateToRecipeViewCommand = new ShowRecipeCommand(this, restaurant, messageStore, factory);
            NavigateToRecipeViewCommand = new RelayCommand(param => ShowRecipeView(param));
            // we can replace it with RelayCommand
            ShowDishesInOrderCommand = new ShowDishesInOrderCommand(this, restaurant, messageStore, factory);
            MessageViewModel = messageViewModel;

            //
            _restaurant = restaurant;
            _factory = factory; // ?? throw new ArgumentNullException ....
            _logger = factory.CreateLogger<MainChefViewModel>();
            _messageStore = messageStore; // ?? throw new ArgumentNullException ....
        }

        public void ShowRecipeView(object? parameter)
        {
            try
            {
                if (parameter is OrderItemViewModel incomingViewModel)
                {
                    _logger?.LogInformation("Start showing recipe...");

                    var chosenDish = this.Orders
                        .SingleOrDefault(o => o.OrderNumber == incomingViewModel.OrderNumber)?.OrderItems
                        .FirstOrDefault(oi => oi.Id == incomingViewModel.Id);

                    if (chosenDish != null)
                    {
                        var viewModel = new ChefLookingAtRecipeViewModel(
                                        chosenDish,
                                        _restaurant,
                                        this,
                                        _messageStore,
                                        _factory);

                        this.CurrentViewModel = viewModel;
                        _logger?.LogInformation("Recipe has been shown successfully.");
                    }
                }
                else
                {
                    _logger?.LogWarning("Invalid parameter type in ShowRecipeCommand");
                }
            }
            catch (ArgumentNullException ane)
            {
                _messageStore.SetMessage(ane.Message, MessageType.Error);
                _logger.LogError(ane.GetExceptionData());
            }
            catch (Exception e)
            {
                _messageStore.SetMessage(e.Message, MessageType.Error);
                _logger.LogError(e.GetExceptionData());
            }
        }
    }
}
