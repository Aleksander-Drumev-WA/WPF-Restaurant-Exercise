using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_Restaurant.Commands;
using WPF_Restaurant.Models;

namespace WPF_Restaurant.ViewModels
{
    public class MenuAndBasketViewModel : BaseViewModel
    {
        private readonly ObservableCollection<DishViewModel> _dishesInMenu;

        public IEnumerable<DishViewModel> DishesInMenu => _dishesInMenu;

        public ICommand ChooseDishCommand { get; }

        public MenuAndBasketViewModel(Restaurant restaurant)
        {
            _dishesInMenu = new ObservableCollection<DishViewModel>();
            _dishesInMenu.Add(new DishViewModel(new Dish("Pizza", @"E:\WPF-Restaurant\Resources\Images\pizza.jpg", "Whatever recipe", 3, "Some ing.", "Some ing. 2")));
            _dishesInMenu.Add(new DishViewModel(new Dish("Burger", @"E:\WPF-Restaurant\Resources\Images\burger.jpg", "Whatever recipe", 3, "Some ing.", "Some ing. 2")));
            _dishesInMenu.Add(new DishViewModel(new Dish("Beef", @"E:\WPF-Restaurant\Resources\Images\beef.jpg", "Whatever recipe", 3, "Some ing.", "Some ing. 2")));
            _dishesInMenu.Add(new DishViewModel(new Dish("Beer", @"E:\WPF-Restaurant\Resources\Images\beer.jpg", "Whatever recipe", 3, "Some ing.", "Some ing. 2")));
            _dishesInMenu.Add(new DishViewModel(new Dish("Ice Cream", @"E:\WPF-Restaurant\Resources\Images\ice-cream.jpg", "Whatever recipe", 3, "Some ing.", "Some ing. 2")));
            _dishesInMenu.Add(new DishViewModel(new Dish("Pasta", @"E:\WPF-Restaurant\Resources\Images\pasta.jpg", "Whatever recipe", 3, "Some ing.", "Some ing. 2")));
            _dishesInMenu.Add(new DishViewModel(new Dish("Garlic Potatoes", @"E:\WPF-Restaurant\Resources\Images\potatoes.jpg", "Whatever recipe", 3, "Some ing.", "Some ing. 2")));
            _dishesInMenu.Add(new DishViewModel(new Dish("Rice", @"E:\WPF-Restaurant\Resources\Images\rice.jpg", "Whatever recipe", 3, "Some ing.", "Some ing. 2")));
            _dishesInMenu.Add(new DishViewModel(new Dish("Salad", @"E:\WPF-Restaurant\Resources\Images\salad.jpg", "Whatever recipe", 3, "Some ing.", "Some ing. 2")));
            _dishesInMenu.Add(new DishViewModel(new Dish("Chocolate Cake", @"E:\WPF-Restaurant\Resources\Images\triple-chocolate-cake.jpg", "Whatever recipe", 3, "Some ing.", "Some ing. 2")));

            ChooseDishCommand = new ChooseDishCommand();
        }
    }
}
