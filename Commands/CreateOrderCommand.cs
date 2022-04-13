using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Restaurant.DTOs;
using WPF_Restaurant.Models;
using WPF_Restaurant.Stores;
using WPF_Restaurant.ViewModels;
using static WPF_Restaurant.Stores.MessageStore;

namespace WPF_Restaurant.Commands
{
    public class CreateOrderCommand : AsyncBaseCommand
    {
        private ObservableCollection<DishViewModel> _chosenDishes;
        private readonly Restaurant _restaurant;
        private readonly MessageStore _messageStore;

        public CreateOrderCommand(ObservableCollection<DishViewModel> chosenDishes, Restaurant restaurant, MessageStore messageStore)
        {
            _chosenDishes = chosenDishes;
            _restaurant = restaurant;
            _messageStore = messageStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                var dishes = _chosenDishes.Select(x => x.Dish).ToList();

                await _restaurant.OrderCreator.CreateOrder(dishes);

                _messageStore.SetMessage("Successfully created an order.", MessageType.Information);
            }
            catch (Exception e)
            {
                _messageStore.SetMessage(e.Message, MessageType.Error);
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter);
        }
    }
}
