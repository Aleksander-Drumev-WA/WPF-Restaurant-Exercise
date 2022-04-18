using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_Restaurant.DataAccess.Data;
using WPF_Restaurant.Extensions;
using WPF_Restaurant.Models;
using WPF_Restaurant.Stores;
using WPF_Restaurant.ViewModels;
using static WPF_Restaurant.Stores.MessageStore;

namespace WPF_Restaurant.Commands
{
    public class CompleteDishCommand : AsyncBaseCommand
    {
        private Restaurant _restaurant;
        private ICommand _loadOrdersCommand;
        private readonly MessageStore _messageStore;
        private readonly ILogger<CompleteDishCommand> _logger;

        public CompleteDishCommand(Restaurant restaurant, ICommand loadOrdersCommand, MessageStore messageStore, ILoggerFactory factory)
        {
            _restaurant = restaurant;
            _loadOrdersCommand = loadOrdersCommand;
            _messageStore = messageStore;
            _logger = factory.CreateLogger<CompleteDishCommand>();
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                if (parameter is ChefLookingAtRecipeViewModel chefLookingAtRecipeViewModel)
                {
                    _logger.LogInformation("Start finishing a dish operation...");
                    await CompleteSingleDish(chefLookingAtRecipeViewModel.DishId, chefLookingAtRecipeViewModel.OrderNumber);
                    _logger.LogInformation("Finishing a dish was successful.");

                }
                else if (parameter is ChefLookingAtOrderItemViewModel chefLookingAtOrderItemViewModel)
                {
                    _logger.LogInformation("Start finishing a dish operation...");
                    await CompleteSingleDish(chefLookingAtOrderItemViewModel.DishId, chefLookingAtOrderItemViewModel.OrderNumber);
                    chefLookingAtOrderItemViewModel.IsCompleted = false;
                    _logger.LogInformation("Finishing a dish was successful.");
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

        private async Task CompleteSingleDish(int dishId, int orderNumber)
        {
            await _restaurant.OrdersProvider.CompleteDish(dishId, orderNumber);
            _loadOrdersCommand.Execute(null);
            _messageStore.SetMessage("Dish completed successfully!", MessageType.Information);
        }
    }
}
