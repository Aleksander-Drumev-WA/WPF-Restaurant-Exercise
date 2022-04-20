using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Restaurant.DataAccess.Data;
using WPF_Restaurant.Extensions;
using WPF_Restaurant.Models;
using WPF_Restaurant.Stores;
using WPF_Restaurant.ViewModels;
using static WPF_Restaurant.Stores.MessageStore;

namespace WPF_Restaurant.Commands
{
    public class ShowDishesInOrderCommand : BaseCommand
    {
        private MainChefViewModel _mainChefViewModel;
        private readonly Restaurant _restaurant;
        private readonly MessageStore _messageStore;
		private readonly ILoggerFactory _factory;
		private readonly ILogger<ShowDishesInOrderCommand> _logger;

		public ShowDishesInOrderCommand(MainChefViewModel mainChefViewModel, Restaurant restaurant, MessageStore messageStore, ILoggerFactory factory)
        {
            _mainChefViewModel = mainChefViewModel;
            _restaurant = restaurant;
            _messageStore = messageStore;
			_factory = factory;
			_logger = factory.CreateLogger<ShowDishesInOrderCommand>();
		}

        public override void Execute(object? parameter)
        {
            try
            {
                if (parameter is int incomingOrderNumber)
                {
                    _logger.LogInformation("Start showing dishes in an order...");
                    var orderWithDishes = _mainChefViewModel.Orders.FirstOrDefault(o => o.OrderNumber == incomingOrderNumber);
                    if (orderWithDishes != null)
                    {
                        var viewModel = new ChefLookingAtOrderViewModel(
                            orderWithDishes.OrderNumber,
                            orderWithDishes.OrderItems,
                            _restaurant,
                            _mainChefViewModel,
                            _messageStore, 
                            _factory,
                            _mainChefViewModel.NotReadyFilterChecked);

                        _mainChefViewModel.CurrentViewModel = viewModel;
                    }
                    _logger.LogInformation("Dishes in order have been shown successfully.");
                }
				else
				{
                    _logger.LogWarning("Invalid parameter type in ShowDishesInOrderCommand");
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
