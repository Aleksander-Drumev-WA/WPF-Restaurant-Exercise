using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Restaurant.DataAccess.Data;
using WPF_Restaurant.Extensions;
using WPF_Restaurant.Models;
using WPF_Restaurant.Stores;
using WPF_Restaurant.ViewModels;
using WPF_Restaurant.ViewModels.Chef;
using WPF_Restaurant.Views;
using static WPF_Restaurant.Stores.MessageStore;

namespace WPF_Restaurant.Commands
{
    // we can delete this if will use RelayCommand
    public class ShowRecipeCommand : BaseCommand
    {
        private MainChefViewModel _mainChefViewModel;
        private readonly Restaurant _restaurant;
        private readonly MessageStore _messageStore;
		private readonly ILoggerFactory _factory;
        private readonly ILogger<ShowRecipeCommand> _logger;

		public ShowRecipeCommand(MainChefViewModel mainChefViewModel, Restaurant restaurant, MessageStore messageStore, ILoggerFactory factory)
        {
            _mainChefViewModel = mainChefViewModel;
            _restaurant = restaurant;
            _messageStore = messageStore;
			_factory = factory;
            _logger = factory?.CreateLogger<ShowRecipeCommand>();
		}

        public override void Execute(object? parameter)
        {
            try
            {
                if (parameter is OrderItemViewModel incomingViewModel)
                {
                    _logger?.LogInformation("Start showing recipe...");

                    var chosenDish = _mainChefViewModel.Orders
                        .SingleOrDefault(o => o.OrderNumber == incomingViewModel.OrderNumber)?.OrderItems
                        .FirstOrDefault(oi => oi.Id == incomingViewModel.Id);

                    if (chosenDish != null)
                    {
                        var viewModel = new ChefLookingAtRecipeViewModel(
                                        chosenDish,
                                        _restaurant,
                                        _mainChefViewModel,
                                        _messageStore, 
                                        _factory);

                        _mainChefViewModel.CurrentViewModel = viewModel;
                        _logger?.LogInformation("Recipe has been shown successfully.");
                    }
                }
				else
				{
                    _logger?.LogWarning("Invalid parameter type in ShowRecipeCommand");
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
