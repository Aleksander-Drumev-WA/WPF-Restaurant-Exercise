using Microsoft.Extensions.Logging;
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
using WPF_Restaurant.Models;
using WPF_Restaurant.Stores;

namespace WPF_Restaurant.ViewModels
{
	public class MenuAndBasketViewModel : BaseViewModel
	{
		private readonly ObservableCollection<DishViewModel> _dishesInMenu;

		private readonly ObservableCollection<DishViewModel> _chosenDishes;

		private RelayCommand _changeQuantityCommand;

		public ObservableCollection<DishViewModel> DishesInMenu => _dishesInMenu;

		public ObservableCollection<DishViewModel> ChosenDishes => _chosenDishes;

		public MessageViewModel MessageViewModel { get; }

		public ICommand ChooseDishCommand { get; }

		public RelayCommand ChangeQuantityCommand
		{
			get
			{
				return _changeQuantityCommand ?? (_changeQuantityCommand = new RelayCommand((obj) =>
				{
					if (obj is object[] parameters)
					{
						if (parameters[0] is Dish dish && parameters[1] is string sign)
						{
							var dishToChange = _chosenDishes.First(d => d.Name == dish.Name);
							if (sign == "+")
							{
								dishToChange.Quantity++;
							}
							else if (sign == "-")
							{

								dishToChange.Quantity--;
							}
						}
					}
				}));
			}
		}

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
			RemoveDishCommand = new RemoveDishCommand(_chosenDishes, messageStore, factory);
			LoadDishesCommand = new LoadDishesCommand(_dishesInMenu, restaurant, messageStore, factory);
			LoadDishesCommand.Execute(null);
			OrderCommand = new CreateOrderCommand(_chosenDishes, restaurant, messageStore, factory);
			NavigateCommand = new NavigateCommand<MainChefViewModel>(navigationStore, mainChefViewModel);
			MessageViewModel = messageViewModel;
		}
	}
}
