using DataAccess.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
using WPF_Restaurant.DataAccess.Data;
using WPF_Restaurant.DataAccess.Data.Providers;
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
		private readonly IHost _host;

		public App()
		{
			_host = Host.CreateDefaultBuilder()
					.UseSerilog((host, loggerConfiguration) =>
					{
						loggerConfiguration.WriteTo.File(@"..\Logs\log.txt", rollingInterval: RollingInterval.Day);
					})
					.ConfigureServices(services =>
					{
						services.AddTransient(s => new RestaurantDbContextFactory(CONNECTION_STRING));
						services.AddTransient<IDishProvider, DatabaseDishProvider>();
						services.AddTransient<IOrderCreator, DatabaseOrderCreator>();
						services.AddTransient<IOrderProvider, DatabaseOrdersProvider>();

						services.AddTransient((s) => new Restaurant(
							"Panorama",
							s.GetRequiredService<IDishProvider>(),
							s.GetRequiredService<IOrderCreator>(),
							s.GetRequiredService<IOrderProvider>()));

						services.AddSingleton<MessageViewModel>();
						services.AddSingleton<MessageStore>();
						services.AddTransient<NavigationStore>();
						

						services.AddTransient<MenuAndBasketViewModel>();
						services.AddSingleton<Func<MenuAndBasketViewModel>>((s) => () => s.GetRequiredService<MenuAndBasketViewModel>());

						services.AddTransient<MainChefViewModel>();
						services.AddSingleton<Func<MainChefViewModel>>((s) => () => s.GetRequiredService<MainChefViewModel>());

						services.AddSingleton<MainViewModel>();
						services.AddSingleton((s) => new MainWindow()
						{
							DataContext = s.GetRequiredService<MainViewModel>(),
						});

						


					}).Build();
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			_host.Start();

			var restaurantDbContextFactory = _host.Services.GetRequiredService<RestaurantDbContextFactory>();
			using (var dbContext = restaurantDbContextFactory.CreateDbContext())
			{
				dbContext.Database.Migrate();
			}

			var mainViewModel = _host.Services.GetRequiredService<MainViewModel>();
			mainViewModel.CurrentViewModel = _host.Services.GetRequiredService<MenuAndBasketViewModel>();

			MainWindow = _host.Services.GetRequiredService<MainWindow>();
			MainWindow.Show();

			base.OnStartup(e);
		}
	}
}
