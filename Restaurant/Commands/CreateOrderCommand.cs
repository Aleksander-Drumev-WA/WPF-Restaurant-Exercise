using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Restaurant.Models;
using WPF_Restaurant.Extensions;
using WPF_Restaurant.Stores;
using WPF_Restaurant.ViewModels;
using static WPF_Restaurant.Stores.MessageStore;
using WPF_Restaurant.DataAccess.Data;
using Models.Models;

namespace WPF_Restaurant.Commands
{
    public class CreateOrderCommand : AsyncBaseCommand
    {
        private ObservableCollection<DishViewModel> _chosenDishes;
        private readonly Restaurant _restaurant;
        private readonly MessageStore _messageStore;
        private readonly ILogger<CreateOrderCommand> _logger;

		public CreateOrderCommand(ObservableCollection<DishViewModel> chosenDishes, Restaurant restaurant, MessageStore messageStore, ILoggerFactory factory)
        {
            _chosenDishes = chosenDishes;
            _restaurant = restaurant;
            _messageStore = messageStore;
            _logger = factory.CreateLogger<CreateOrderCommand>();
		}

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                _logger.LogInformation("Creating order...");
                var dishes = _chosenDishes.Select(x => new CartItem(x.Dish, x.Quantity));

                await _restaurant.OrderCreator.CreateOrder(dishes);

                _messageStore.SetMessage("Successfully created an order.", MessageType.Information);
                _logger.LogInformation("Successfully created an order.");
            }
            catch (Exception e)
            {
                _messageStore.SetMessage(e.Message, MessageType.Error);
                _logger.LogError(e.GetExceptionData());
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter);
        }
    }
}
