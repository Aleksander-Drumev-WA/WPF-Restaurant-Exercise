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
        private Restaurant _restaurant;
        private readonly MessageStore _messageStore;
        private readonly ILogger<LoadDishesCommand> _logger;

        public LoadDishesCommand(ObservableCollection<DishViewModel> dishesInMenu, Restaurant restaurant, MessageStore messageStore, ILoggerFactory factory)
        {
            _dishesInMenu = dishesInMenu;
            _restaurant = restaurant;
            _messageStore = messageStore;
            _logger = factory.CreateLogger<LoadDishesCommand>();
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                _logger.LogInformation("Start loading dishes...");
                var dishes = await _restaurant.DishProvider.GetAllDishes();
                await Task.Delay(2000);

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
