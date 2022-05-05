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
using WPF_Restaurant.ViewModels.Customer;

namespace WPF_Restaurant.Commands
{
    public class CreateOrderCommand : AsyncBaseCommand
    {
        private ObservableCollection<DishViewModel> _chosenDishes;
        private readonly Restaurant _restaurant;
        private readonly IMessageStore _messageStore;
        private readonly ILogger _logger;

        // Why we use Restaurant restaurant, if we need only IOrderCreator?
        public CreateOrderCommand(ObservableCollection<DishViewModel> chosenDishes, Restaurant restaurant, IMessageStore messageStore, ILogger logger)
        {
            _chosenDishes = chosenDishes;
            _restaurant = restaurant;
            _messageStore = messageStore;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                _logger?.LogInformation("Creating order...");
                var dishes = _chosenDishes.Select(x => new CartItem(x.Dish, x.Quantity));

                await _restaurant.OrderCreator.CreateOrder(dishes);

                _messageStore?.SetMessage("Successfully created an order.", MessageType.Information);
                _logger?.LogInformation("Successfully created an order.");
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
