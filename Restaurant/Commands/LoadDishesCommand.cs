using DataAccess.Abstractions;
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
using WPF_Restaurant.ViewModels.Customer;
using static WPF_Restaurant.Stores.MessageStore;

namespace WPF_Restaurant.Commands
{
    public class LoadDishesCommand : AsyncBaseCommand
    {
        private readonly ObservableCollection<DishViewModel> _dishesInMenu;
        private readonly IDishProvider _dishProvider;
        private readonly IMessageStore _messageStore;
        private readonly ILogger _logger;

        // 1. Simplify ILogger<LoadDishesCommand> - we don't use any special methods of ILogger<out TCategoryName> (PS. ILogger<out TCategoryName> don't has any special methods)
        // 2. Why we use Restaurant restaurant, if we need only IDishProvider?
        // 3. Why we use MessageStore, but not IMessageStore - IMessageStore allows to replace message service with other implementation.
        public LoadDishesCommand(ObservableCollection<DishViewModel> dishesInMenu, IDishProvider dishProvider, IMessageStore messageStore, ILogger logger)
        {
            _dishesInMenu = dishesInMenu ?? throw new ArgumentNullException(nameof(dishesInMenu));
            _dishProvider = dishProvider ?? throw new ArgumentNullException(nameof(dishProvider));
            _messageStore = messageStore ?? throw new ArgumentNullException(nameof(messageStore));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                _logger.LogInformation("Start loading dishes...");
                var dishes = await _dishProvider.GetAllDishes();

                foreach(var dish in dishes.Select(d => new DishViewModel(d))) {
                    _dishesInMenu.Add(dish);
                }
                _logger.LogInformation("Dishes have been loaded successfully.");

            }
            catch (Exception e)
            {
                _messageStore.SetMessage(e.Message, MessageType.Error);
                _logger.LogError(e.GetExceptionData());
            }
        }
    }
}
