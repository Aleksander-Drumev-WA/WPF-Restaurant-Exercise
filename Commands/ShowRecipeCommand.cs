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
using WPF_Restaurant.Views;

namespace WPF_Restaurant.Commands
{
    public class ShowRecipeCommand : BaseCommand
    {
        private MainChefViewModel _mainChefViewModel;
        private readonly Restaurant _restaurant;

        public ShowRecipeCommand(MainChefViewModel mainChefViewModel, Restaurant restaurant)
        {
            _mainChefViewModel = mainChefViewModel;
            _restaurant = restaurant;
        }

        public override void Execute(object? parameter)
        {
            try
            {
                if (parameter is OrderItemViewModel)
                {
                    var incomingViewModel = (OrderItemViewModel)parameter;

                    var chosenDish = _mainChefViewModel.Orders
                        .SingleOrDefault(o => o.OrderNumber == incomingViewModel.OrderNumber)?.OrderItems
                        .FirstOrDefault(oi => oi.Id == incomingViewModel.Id);

                    if (chosenDish != null)
                    {
                        var viewModel = new ChefLookingAtRecipeViewModel(chosenDish, incomingViewModel.Id, _restaurant, _mainChefViewModel.Orders);

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
