using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Restaurant.Extensions;
using WPF_Restaurant.Models;
using WPF_Restaurant.Stores;
using WPF_Restaurant.ViewModels;
using WPF_Restaurant.ViewModels.Customer;
using static WPF_Restaurant.Stores.MessageStore;

namespace WPF_Restaurant.Commands
{
	// It's too simple command, I think, we can replace it with RelayCommand.
	// My arguments:
	// 1. The "action" is too simple.
	// 2. Using RelayCommand we can avoid testing separated class RemoveDishCommand.
	// Any objections?
	public class RemoveDishCommand : BaseCommand
	{
		private ObservableCollection<DishViewModel> _chosenDishes;
		private readonly IMessageStore _messageStore;
		private readonly ILogger _logger;

		public RemoveDishCommand(ObservableCollection<DishViewModel> chosenDishes, IMessageStore messageStore, ILogger logger)
		{
			_chosenDishes = chosenDishes;
			_messageStore = messageStore;
			_logger = logger;
		}

		public override void Execute(object? parameter)
		{
			try
			{
				_logger.LogInformation("Start removing dish...");
				// I'm trying to understand: why we get a instance of Dish.
				// If we change
				// Command = "{Binding DataContext.RemoveDishCommand, ElementName=ChosenList}"
				// CommandParameter = "{Binding Dish}" >
				// to
				// Command="{Binding DataContext.RemoveDishCommand, ElementName=ChosenList}"
				// CommandParameter = "{Binding}" >
				// we can... solve it yourself :-)
				if (parameter is Dish dish)
				{
					// Search by name... Ok, sometimes...
					// but searching by int is more efficient.
					// Can we use searching by int here?
					var dishToRemove = _chosenDishes.First(cd => cd.Name == dish.Name);
					_chosenDishes.Remove(dishToRemove);
					_messageStore.SetMessage("Dish has been removed.", MessageType.Information);
					_logger.LogInformation("Dish has been removed successfully.");
				}
				else
				{
					throw new ArgumentException("Wrong parameter passed to RemoveDishCommand");
				}
			}
			// I've never seen using ArgumentNullException in catch.
			// why do we need this block?
			catch (ArgumentNullException ane)
			{
				_logger.LogError(ane.GetExceptionData());
				_messageStore.SetMessage(ane.Message, MessageType.Error);
			}
			catch (Exception e)
			{
				_logger.LogError(e.GetExceptionData());
				_messageStore.SetMessage(e.Message, MessageType.Error);
			}
		}
	}
}
