using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPF_Restaurant.Models;
using WPF_Restaurant.ViewModels;

namespace WPF_Restaurant.Commands
{
    public class ChooseDishCommand : BaseCommand
    {
        private readonly ObservableCollection<DishViewModel> _chosenDishes;
        private DishViewModel _dishViewModel;
        private Dish? _dish;

        public DishViewModel ChosenDish => _dishViewModel;

        public ChooseDishCommand(ObservableCollection<DishViewModel> chosenDishes)
        {
            _chosenDishes = chosenDishes;
        }

        public override void Execute(object? parameter)
        {
            try
            {
                _dish = (Dish?)parameter;
                _dishViewModel = new DishViewModel(_dish);
                _chosenDishes.Add(_dishViewModel);
            }
            // Logging later
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message, "Error occured.", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Unexpected error occured.", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }
}
