using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPF_Restaurant.Models;
using WPF_Restaurant.Services.Data;
using WPF_Restaurant.ViewModels;

namespace WPF_Restaurant
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string CONNECTION_STRING = "Data Source=restaurant.db";
        private readonly Restaurant _restaurant;

        public App()
        {
            _restaurant = new Restaurant("Panorama");
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var options = new DbContextOptionsBuilder().UseSqlite(CONNECTION_STRING).Options;
            using (var dbContext = new RestaurantDbContext(options))
            {
                dbContext.Database.Migrate();
            }

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_restaurant)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
