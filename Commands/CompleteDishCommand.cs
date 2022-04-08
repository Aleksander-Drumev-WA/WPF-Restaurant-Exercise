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
    public class CompleteDishCommand : AsyncBaseCommand
    {
        private Restaurant _restaurant;
        private ICommand _loadOrdersCommand;

        public CompleteDishCommand(Restaurant restaurant, ICommand loadOrdersCommand)
        {
            _restaurant = restaurant;
            _loadOrdersCommand = loadOrdersCommand;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                if (parameter is ChefLookingAtRecipeViewModel)
                {
                    var viewModel = (ChefLookingAtRecipeViewModel)parameter;
                    await _restaurant.OrdersProvider.CompleteDish(viewModel.DishId, viewModel.OrderNumber);
                    _loadOrdersCommand.Execute(null);
                    MessageBox.Show("Dish completed successfully!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
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
