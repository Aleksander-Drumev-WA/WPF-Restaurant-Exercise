using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Restaurant.Models;
using WPF_Restaurant.ViewModels;

namespace WPF_Restaurant.Commands
{
    public class ShowDishesInOrderCommand : BaseCommand
    {
        private MainChefViewModel _mainChefViewModel;
        private readonly Restaurant _restaurant;

        public ShowDishesInOrderCommand(MainChefViewModel mainChefViewModel, Restaurant restaurant)
        {
            _mainChefViewModel = mainChefViewModel;
            _restaurant = restaurant;
        }

        public override void Execute(object? parameter)
        {
            try
            {
                if (parameter is int incomingOrderNumber)
                {
                    var orderWithDishes = _mainChefViewModel.Orders.FirstOrDefault(o => o.OrderNumber == incomingOrderNumber);
                    if (orderWithDishes != null)
                    {
                        var viewModel = new ChefLookingAtOrderViewModel(orderWithDishes.OrderNumber, orderWithDishes.OrderItems, _restaurant, _mainChefViewModel);

                        _mainChefViewModel.CurrentViewModel = viewModel;
                    }
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
