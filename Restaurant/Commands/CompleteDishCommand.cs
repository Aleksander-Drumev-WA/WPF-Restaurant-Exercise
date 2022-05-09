using DataAccess.Abstractions;
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
using WPF_Restaurant.ViewModels.Chef;
using static WPF_Restaurant.Stores.MessageStore;

namespace WPF_Restaurant.Commands
{
    public class CompleteDishCommand : AsyncBaseCommand
    {
        private IOrderProvider _orderProvider;
        private ICommand _loadOrdersCommand;
        private readonly IMessageStore _messageStore;
        private readonly ILogger _logger;
        private readonly MainChefViewModel _mainChefViewModel;

        public CompleteDishCommand(IOrderProvider orderProvider, ICommand loadOrdersCommand, IMessageStore messageStore, ILogger logger, MainChefViewModel mainChefViewModel)
        {
            _orderProvider = orderProvider;
            _loadOrdersCommand = loadOrdersCommand;
            _messageStore = messageStore;
            _logger = logger;
            _mainChefViewModel = mainChefViewModel;
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

                _messageStore.SetMessage("Dish has been finished.", MessageType.Information);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetExceptionData());
                _messageStore.SetMessage(e.Message, MessageType.Error);
            }
        }

        private async Task CompleteSingleDish(int dishId, int orderNumber)
        {
            await _orderProvider.CompleteDish(dishId, orderNumber);
            _loadOrdersCommand.Execute(_mainChefViewModel);
            _messageStore?.SetMessage("Dish completed successfully!", MessageType.Information);
        }
    }
}
