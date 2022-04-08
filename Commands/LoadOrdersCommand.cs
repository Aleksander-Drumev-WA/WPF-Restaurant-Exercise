using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Restaurant.Models;
using WPF_Restaurant.ViewModels;

namespace WPF_Restaurant.Commands
{
    public class LoadOrdersCommand : AsyncBaseCommand
    {
        private ObservableCollection<OrderViewModel> _orders;
        private Restaurant _restaurant;

        public LoadOrdersCommand(ObservableCollection<OrderViewModel> orders, Restaurant restaurant)
        {
            _orders = orders;
            _restaurant = restaurant;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                _orders.Clear();
                var orders = await _restaurant.OrdersProvider.GetAllOrders();
                await Task.Delay(2000);

                foreach (var order in orders)
                {
                    var orderViewModel = new OrderViewModel(order);
                    _orders.Add(orderViewModel);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Failed to load orders.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
