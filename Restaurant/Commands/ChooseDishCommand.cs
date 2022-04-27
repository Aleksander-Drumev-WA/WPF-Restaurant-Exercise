using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPF_Restaurant.Models;
using WPF_Restaurant.Stores;
using WPF_Restaurant.ViewModels;
using WPF_Restaurant.Extensions;
using static WPF_Restaurant.Stores.MessageStore;

namespace WPF_Restaurant.Commands
{
	public class ChooseDishCommand : BaseCommand
	{
		private readonly ObservableCollection<DishViewModel> _chosenDishes;
		private readonly MessageStore _messageStore;
		private readonly ILogger<ChooseDishCommand> _logger;

		public ChooseDishCommand(ObservableCollection<DishViewModel> chosenDishes, MessageStore messageStore, ILoggerFactory factory)
		{
			_chosenDishes = chosenDishes;
			_messageStore = messageStore;

			_logger = factory.CreateLogger<ChooseDishCommand>();
		}

		public override void Execute(object? parameter)
		{
			try
			{
				_logger.LogInformation("Begin choosing a dish...");
				if (parameter is Dish dish)
				{
					_chosenDishes.Add(new DishViewModel(dish));
					_logger.LogInformation("Choosing dish completed successfully.");
				}
				else
				{
					throw new ArgumentException("Wrong parameter passed to ChooseDishCommand");
				}
			}
			// Logging later
			catch (ArgumentNullException ex)
			{
				_messageStore.SetMessage(ex.Message, MessageType.Error);
				_logger.LogError(ex.GetExceptionData());
			}
			catch (Exception e)
			{
				_messageStore.SetMessage(e.Message, MessageType.Error);
				_logger.LogError(e.GetExceptionData());

			}
		}
	}
}
