using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using WPF_Restaurant.Commands;
using WPF_Restaurant.DataAccess.Data;
using WPF_Restaurant.Models;
using WPF_Restaurant.Stores;
using WPF_Restaurant.ViewModels.Common;
using WPF_Restaurant.ViewModels.Customer;


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

		// TODO: ClearMessage into - Relay
		public MenuAndBasketViewModel(
			Restaurant restaurant,
			ICommand navigateCommand,
			IMessageStore messageStore,
			MessageViewModel messageViewModel,
			ILoggerFactory factory)
		{
			_dishesInMenu = new ObservableCollection<DishViewModel>();
			_chosenDishes = new ObservableCollection<DishViewModel>();
			ChooseDishCommand = new ChooseDishCommand(_chosenDishes, messageStore, factory.CreateLogger<ChooseDishCommand>());
			RemoveDishCommand = new RemoveDishCommand(_chosenDishes, messageStore, factory.CreateLogger<RemoveDishCommand>());
			LoadDishesCommand = new LoadDishesCommand(_dishesInMenu, restaurant.DishProvider, messageStore, factory.CreateLogger<LoadDishesCommand>());
			IncreaseQuantityCommand = new RelayCommand((param) => ExecuteChangeQuantityCommand(1, param));
			DecreaseQuantityCommand = new RelayCommand((param) => ExecuteChangeQuantityCommand(-1, param), (param) => CanChangeQuantity(param));
			LoadDishesCommand.Execute(null);
			OrderCommand = new CreateOrderCommand(_chosenDishes, restaurant.OrderCreator, messageStore, factory.CreateLogger<CreateOrderCommand>());
			NavigateCommand = navigateCommand;
			MessageViewModel = messageViewModel;
		}
		private void ExecuteChangeQuantityCommand(int amount, object param)
		{
			if (param is DishViewModel dishViewModel)
			{
				var newQuantity = dishViewModel.Quantity + amount;
				dishViewModel.Quantity = newQuantity < 1 ? 1 : newQuantity;
			}
		}

		private bool CanChangeQuantity(object param)
		{
			if (param is DishViewModel dishViewModel)
			{
				return dishViewModel.Quantity <= 1 ? false : true;
			}
			else
			{
				return false;
			}
		}
	}
}
