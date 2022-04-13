using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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

        public CompleteDishCommand(Restaurant restaurant, ICommand loadOrdersCommand, MessageStore messageStore)
        {
            _restaurant = restaurant;
            _loadOrdersCommand = loadOrdersCommand;
            _messageStore = messageStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                if (parameter is ChefLookingAtRecipeViewModel chefLookingAtRecipeViewModel)
                {
                    await CompleteSingleDish(chefLookingAtRecipeViewModel.DishId, chefLookingAtRecipeViewModel.OrderNumber);
                }
                else if (parameter is ChefLookingAtOrderItemViewModel chefLookingAtOrderItemViewModel)
                {
                    await CompleteSingleDish(chefLookingAtOrderItemViewModel.DishId, chefLookingAtOrderItemViewModel.OrderNumber);
                    chefLookingAtOrderItemViewModel.IsCompleted = false;
                }
            }
            catch (ArgumentNullException ane)
            {
                _messageStore.SetMessage(ane.Message, MessageType.Error);
            }
            catch (Exception e)
            {
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
