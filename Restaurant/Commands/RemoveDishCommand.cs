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
	public class RemoveDishCommand : BaseCommand
	{
		private ObservableCollection<DishViewModel> _chosenDishes;
		private readonly IMessageStore _messageStore;
		private readonly ILogger _logger;

		public RemoveDishCommand(ObservableCollection<DishViewModel> chosenDishes, IMessageStore messageStore, ILogger logger)
		{
			_chosenDishes = chosenDishes;
			_messageStore = messageStore;
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public override void Execute(object? parameter)
		{
			try
			{
				_logger.LogInformation("Start removing dish...");
				if (parameter is Dish dish)
				{
					var dishToRemove = _chosenDishes.First(cd => cd.Name == dish.Name);
					_chosenDishes.Remove(dishToRemove);
					_messageStore?.SetMessage("Dish has been removed.", MessageType.Information);
					_logger?.LogInformation("Dish has been removed successfully.");
				}
				else
				{
					throw new ArgumentException("Wrong parameter passed to RemoveDishCommand");
				}
			}
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
