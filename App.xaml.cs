using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPF_Restaurant.Models;

namespace WPF_Restaurant
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var restaurant = new Restaurant("Panorama");

            restaurant.CreateOrder(new Order(
                new List<Dish>
                {
                    new Dish("Pizza", @"E:\WPF-Restaurant\Resources\Images\pizza.jpg", "Whatever recipe", 3, "Something delicious", "Whatever"),
                    new Dish("Beer", @"E:\WPF-Restaurant\Resources\Images\beer.jpg", "Whatever recipe", 3, "Something delicious", "Whatever"),
                    new Dish("Burger", @"E:\WPF-Restaurant\Resources\Images\burger.jpg", "Whatever recipe", 3, "Something delicious", "Whatever"),
                }));

            restaurant.CreateOrder(new Order(
                new List<Dish>
                {
                    new Dish("Pizza", @"E:\WPF-Restaurant\Resources\Images\pizza.jpg", "Whatever recipe", 3, "Something delicious", "Whatever"),
                    new Dish("Pizza", @"E:\WPF-Restaurant\Resources\Images\beer.jpg", "Whatever recipe", 3, "Something delicious", "Whatever"),
                    new Dish("Pizza", @"E:\WPF-Restaurant\Resources\Images\burger.jpg", "Whatever recipe", 3, "Something delicious", "Whatever"),
                }));

            var order = restaurant.GetOrderByNumber(0);

            base.OnStartup(e);
        }
    }
}
