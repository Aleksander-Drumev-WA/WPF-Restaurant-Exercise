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

		private ILogger _logger;
		private IMessageStore _messageStore;

		public ObservableCollection<OrderViewModel> Orders => _orders;

		public ICommand NavigateCommand { get; }

		public ICommand LoadOrdersCommand { get; }

		public ICommand CompleteDishCommand { get; }

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
			_restaurant = restaurant;
			_logger = factory.CreateLogger<MainChefViewModel>();
			_messageStore = messageStore;

			CompleteDishCommand = new RelayCommand(async param => await CompleteDish(param));
			NavigateCommand = navigateCommand;
			LoadOrdersCommand = new LoadOrdersCommand(_orders, restaurant.OrdersProvider, messageStore, factory.CreateLogger<LoadOrdersCommand>());
			LoadOrdersCommand.Execute(this);
			NavigateToRecipeViewCommand = new RelayCommand(param => ShowRecipeView(param));
			ShowDishesInOrderCommand = new ShowDishesInOrderCommand(this, restaurant.OrdersProvider, messageStore, factory);
			MessageViewModel = messageViewModel;
		}

		private void ShowRecipeView(object? parameter)
		{
			try
			{
				if (parameter is OrderItemViewModel incomingViewModel)
				{
					_logger.LogInformation("Start showing recipe...");

					var viewModel = new ChefLookingAtRecipeViewModel(
									incomingViewModel,
									CompleteDishCommand);

					this.CurrentViewModel = viewModel;
					_logger.LogInformation("Recipe has been shown successfully.");

				}
				else
				{
					_logger.LogWarning("Invalid parameter type in ShowRecipeCommand");
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

		private async Task CompleteDish(object param)
		{
			try
			{
				if (param is ChefLookingAtRecipeViewModel chefLookingAtRecipeViewModel)
				{
					_logger.LogInformation("Start finishing a dish operation...");
					await FinishSingleDish(chefLookingAtRecipeViewModel.DishId, chefLookingAtRecipeViewModel.OrderNumber);
					_logger.LogInformation("Finishing a dish was successful.");

				}
				else if (param is ChefLookingAtOrderItemViewModel chefLookingAtOrderItemViewModel)
				{
					_logger.LogInformation("Start finishing a dish operation...");
					await FinishSingleDish(chefLookingAtOrderItemViewModel.DishId, chefLookingAtOrderItemViewModel.OrderNumber);
					chefLookingAtOrderItemViewModel.IsCompleted = false;
					_logger.LogInformation("Finishing a dish was successful.");
				}
			}
			catch (Exception e)
			{
				_logger.LogError(e.GetExceptionData());
				_messageStore.SetMessage(e.Message, MessageType.Error);
			}
		}

		private async Task FinishSingleDish(int dishId, int orderNumber)
		{
			await _restaurant.OrdersProvider.CompleteDish(dishId, orderNumber);
			LoadOrdersCommand.Execute(this);
			_messageStore.SetMessage("Dish completed successfully!", MessageType.Information);
		}
	}
}
