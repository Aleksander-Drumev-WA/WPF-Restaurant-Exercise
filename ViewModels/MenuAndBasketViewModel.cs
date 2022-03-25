using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_Restaurant.Commands;
using WPF_Restaurant.Models;

namespace WPF_Restaurant.ViewModels
{
    public class MenuAndBasketViewModel : BaseViewModel
    {
        private readonly ObservableCollection<DishViewModel> _dishesInMenu;

        private readonly ObservableCollection<DishViewModel> _chosenDishes;

        public IEnumerable<DishViewModel> DishesInMenu => _dishesInMenu;

        public IEnumerable<DishViewModel> ChosenDishes => _chosenDishes;

        public ICommand ChooseDishCommand { get; }

        public MenuAndBasketViewModel(Restaurant restaurant)
        {
            _dishesInMenu = new ObservableCollection<DishViewModel>();
            _chosenDishes = new ObservableCollection<DishViewModel>();
            _dishesInMenu.Add(new DishViewModel(new Dish("Pizza", @"E:\WPF-Restaurant\Resources\Images\pizza.jpg", "Whatever recipe", 1, "Pepperoni", "Mushroom", "Onion", "Olives", "Mozzarella")));
            _dishesInMenu.Add(new DishViewModel(new Dish("Burger", @"E:\WPF-Restaurant\Resources\Images\burger.jpg", "Whatever recipe", 1, "Beef", "Egg", "Onion", "Mayonnaise", "Iceberg lettuce leaves", "Tomato", "Cheese")));
            _dishesInMenu.Add(new DishViewModel(new Dish("Beef", @"E:\WPF-Restaurant\Resources\Images\beef.jpg", "Whatever recipe", 1, "Beef")));
            _dishesInMenu.Add(new DishViewModel(new Dish("Beer", @"E:\WPF-Restaurant\Resources\Images\beer.jpg", "Whatever recipe", 1, "Beer")));
            _dishesInMenu.Add(new DishViewModel(new Dish("Ice Cream", @"E:\WPF-Restaurant\Resources\Images\ice-cream.jpg", "Whatever recipe", 1, "Chocolate", "Vanillia", "Oreo")));
            _dishesInMenu.Add(new DishViewModel(new Dish("Pasta", @"E:\WPF-Restaurant\Resources\Images\pasta.jpg", "Whatever recipe", 1, "Pasta", "Onion", "Garlic", "Carrots", "Sweet paprika", "Parsley")));
            _dishesInMenu.Add(new DishViewModel(new Dish("Garlic Potatoes", @"E:\WPF-Restaurant\Resources\Images\potatoes.jpg", "Whatever recipe", 1, "Potatoes", "Garlic")));
            _dishesInMenu.Add(new DishViewModel(new Dish("Rice", @"E:\WPF-Restaurant\Resources\Images\rice.jpg", "Whatever recipe", 1, "Carrots", "Chicken breast", "Cinnamon stick", "Lemon zest", "")));
            _dishesInMenu.Add(new DishViewModel(new Dish("Salad", @"E:\WPF-Restaurant\Resources\Images\salad.jpg", "Whatever recipe", 1, "Tomatoes", "Cucumbers", "Onion", "Corn")));
            _dishesInMenu.Add(new DishViewModel(new Dish("Chocolate Cake", @"E:\WPF-Restaurant\Resources\Images\triple-chocolate-cake.jpg", "Whatever recipe", 1, "Chocolate", "Sugar", "Milk", "")));
            ChooseDishCommand = new ChooseDishCommand(_chosenDishes);
        }
    }
}
