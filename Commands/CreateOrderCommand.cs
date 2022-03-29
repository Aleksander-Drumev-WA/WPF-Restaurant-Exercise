using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Restaurant.DTOs;
using WPF_Restaurant.Models;
using WPF_Restaurant.ViewModels;

namespace WPF_Restaurant.Commands
{
    public class CreateOrderCommand : AsyncBaseCommand
    {
        private ObservableCollection<DishViewModel> _chosenDishes;
        private readonly Restaurant _restaurant;

        public CreateOrderCommand(ObservableCollection<DishViewModel> chosenDishes, Restaurant restaurant)
        {
            _chosenDishes = chosenDishes;
            _restaurant = restaurant;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                var orderId = await _restaurant.OrderCreator.CreateOrder();

                var dishesForOrder = _chosenDishes.Select(cd => new DishInOrderDTO()
                {
                    Name = cd.Name,
                    Quantity = cd.Quantity,
                    IsReady = false,
                    OrderId = orderId
                });

                await _restaurant.OrderCreator.PopulateOrder(dishesForOrder);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Failed to create a order.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter);
        }
    }
}
