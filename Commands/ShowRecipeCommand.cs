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
                var values = (object[])parameter;

                var dishId = (int)values[0];
                var orderId = (int)values[1];

                var chosenDish = _mainChefViewModel.Orders.SelectMany(x => x.OrderItems.Where(oi => oi.Id == dishId && oi.OrderNumber == orderId)).FirstOrDefault();
                if (chosenDish != null)
                {
                    var viewModel = new ChefLookingAtRecipeViewModel(chosenDish.OrderNumber, dishId, chosenDish.Name, chosenDish.Recipe, _restaurant, _mainChefViewModel.Orders);

                    _mainChefViewModel.CurrentViewModel = viewModel;
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
