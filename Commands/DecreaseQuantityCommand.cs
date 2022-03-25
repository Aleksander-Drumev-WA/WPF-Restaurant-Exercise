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
    public class DecreaseQuantityCommand : BaseCommand
    {
        private ObservableCollection<DishViewModel> _chosenDishes;

        public DecreaseQuantityCommand(ObservableCollection<DishViewModel> chosenDishes)
        {
            _chosenDishes = chosenDishes;
        }

        public override void Execute(object? parameter)
        {
            try
            {
                var dishViewModel = new DishViewModel((Dish)parameter);

                var modelToChange = _chosenDishes.FirstOrDefault(cd => cd.Name == dishViewModel.Name);
                modelToChange.Quantity--;
            }
            catch (ArgumentNullException ane)
            {
                MessageBox.Show(ane.Message, "Error occured.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Unexpected error occured.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
