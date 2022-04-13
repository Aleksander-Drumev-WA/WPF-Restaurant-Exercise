using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Restaurant.Models;
using WPF_Restaurant.Stores;
using WPF_Restaurant.ViewModels;
using static WPF_Restaurant.Stores.MessageStore;

namespace WPF_Restaurant.Commands
{
    public class LoadDishesCommand : AsyncBaseCommand
    {
        private readonly ObservableCollection<DishViewModel> _dishesInMenu;
        private Restaurant _restaurant;
        private readonly MessageStore _messageStore;

        public LoadDishesCommand(ObservableCollection<DishViewModel> dishesInMenu, Restaurant restaurant, MessageStore messageStore)
        {
            _dishesInMenu = dishesInMenu;
            _restaurant = restaurant;
            _messageStore = messageStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                var dishes = await _restaurant.DishProvider.GetAllDishes();
                await Task.Delay(2000);

                foreach(var dish in dishes.Select(d => new DishViewModel(d))) {
                    _dishesInMenu.Add(dish);
                }
            }
            catch (Exception e)
            {
                _messageStore.SetMessage(e.Message, MessageType.Error);
            }
        }
    }
}
