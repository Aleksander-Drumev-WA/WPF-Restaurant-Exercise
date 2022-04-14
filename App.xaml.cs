﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPF_Restaurant.Commands;
using WPF_Restaurant.Models;
using WPF_Restaurant.Services.Data;
using WPF_Restaurant.Services.Data.Providers;
using WPF_Restaurant.Stores;
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
        private readonly RestaurantDbContextFactory _restaurantDbContextFactory;
        private readonly NavigationStore _navigationStore;
        private readonly MessageStore _messageStore;
        private readonly MessageViewModel _messageViewModel;

        public App()
        {
            _restaurantDbContextFactory = new RestaurantDbContextFactory(CONNECTION_STRING);
            var databaseDishProvider = new DatabaseDishProvider(_restaurantDbContextFactory);
            var databaseOrderCreator = new DatabaseOrderCreator(_restaurantDbContextFactory);
            var databaseOrdersProvider = new DatabaseOrdersProvider(_restaurantDbContextFactory);

            _restaurant = new Restaurant("Panorama", databaseDishProvider, databaseOrderCreator, databaseOrdersProvider);
            _navigationStore = new NavigationStore();
            _messageStore = new MessageStore();
            _messageViewModel = new MessageViewModel(_messageStore);
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                var loggingConfiguration = new LoggerConfiguration()
                .WriteTo.File(@"...\Logs\log.txt", rollingInterval: RollingInterval.Day);
                builder.AddSerilog(loggingConfiguration.CreateLogger());
            });
            ILogger<ChooseDishCommand> chooseDishCommandLogger = loggerFactory.CreateLogger<ChooseDishCommand>();
            chooseDishCommandLogger.LogInformation("TEsting!");

            _navigationStore.CurrentViewModel = MenuAndBasketViewModel.LoadViewModel(_restaurant, _navigationStore, MakeMainChefViewModel, _messageStore, _messageViewModel);
            using (var dbContext = _restaurantDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private MainChefViewModel MakeMainChefViewModel()
        {
            return MainChefViewModel.LoadViewModel(_navigationStore, MakeMenuAndBasketViewModel, _restaurant, _messageStore, _messageViewModel);
        }

        private MenuAndBasketViewModel MakeMenuAndBasketViewModel()
        {
            return MenuAndBasketViewModel.LoadViewModel(_restaurant, _navigationStore, MakeMainChefViewModel, _messageStore, _messageViewModel);
                 
        }
    }
}
