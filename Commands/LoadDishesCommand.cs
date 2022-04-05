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
    public class LoadDishesCommand : AsyncBaseCommand
    {
        private readonly List<DishViewModel> _dishesInMenu;
        private Restaurant _restaurant;

        public LoadDishesCommand(List<DishViewModel> dishesInMenu, Restaurant restaurant)
        {
            _dishesInMenu = dishesInMenu;
            _restaurant = restaurant;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                var dishes = await _restaurant.DishProvider.GetAllDishes();
                Task.Delay(2000);

                _dishesInMenu.AddRange(dishes.Select(d => new DishViewModel(d)));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Failed to load dishes.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
