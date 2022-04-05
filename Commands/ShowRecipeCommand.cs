using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Restaurant.Stores;
using WPF_Restaurant.ViewModels;
using WPF_Restaurant.Views;

namespace WPF_Restaurant.Commands
{
    public class ShowRecipeCommand : BaseCommand
    {
        private MainChefViewModel _mainChefViewModel;


        public ShowRecipeCommand(MainChefViewModel mainChefViewModel)
        {
            _mainChefViewModel = mainChefViewModel;
        }

        public override void Execute(object? parameter)
        {
            try
            {
                var dishId = (int)parameter;

                var chosenDish = _mainChefViewModel.Orders.Select(x => x.OrderItems.FirstOrDefault(oi => oi.Id == dishId)).FirstOrDefault();
                var viewModel = new ChefLookingAtRecipeViewModel(chosenDish.OrderNumber, chosenDish.Name, chosenDish.Recipe);

                _mainChefViewModel.CurrentViewModel = viewModel;
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
