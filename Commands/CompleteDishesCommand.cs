using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_Restaurant.Models;
using WPF_Restaurant.ViewModels;

namespace WPF_Restaurant.Commands
{
    public class CompleteDishesCommand : AsyncBaseCommand
    {
        private Restaurant _restaurant;
        private ICommand _loadOrdersCommand;

        public CompleteDishesCommand(Restaurant restaurant, ICommand loadOrdersCommand)
        {
            _restaurant = restaurant;
            _loadOrdersCommand = loadOrdersCommand;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                var values = (object[])parameter;
                var dishId = (int)values[0];
                var orderNumber = (int)values[1];

                await _restaurant.OrdersProvider.CompleteDish(dishId, orderNumber);
                _loadOrdersCommand.Execute(null);
            }
            catch (ArgumentNullException ane)
            {
                MessageBox.Show(ane.Message, "No item has been selected.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Unexpected error occured.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
