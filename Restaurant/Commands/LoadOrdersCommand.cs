using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Restaurant.DataAccess.Data;
using WPF_Restaurant.Extensions;
using WPF_Restaurant.Models;
using WPF_Restaurant.Stores;
using WPF_Restaurant.ViewModels;
using static WPF_Restaurant.Stores.MessageStore;

namespace WPF_Restaurant.Commands
{
	public class LoadOrdersCommand : AsyncBaseCommand
	{
		private ObservableCollection<OrderViewModel> _orders;
		private Restaurant _restaurant;
		private readonly MessageStore _messageStore;
		private readonly ILogger<LoadOrdersCommand> _logger;

		public LoadOrdersCommand(ObservableCollection<OrderViewModel> orders, Restaurant restaurant, MessageStore messageStore, ILoggerFactory factory)
		{
			_orders = orders;
			_restaurant = restaurant;
			_messageStore = messageStore;
			_logger = factory.CreateLogger<LoadOrdersCommand>();
		}


		public override async Task ExecuteAsync(object? parameter)
		{
			try
			{
				_logger.LogInformation("Start loading orders...");
				_orders.Clear();
				IEnumerable<Order> orders;

				if (parameter is MainChefViewModel filters)
				{
					orders = await _restaurant.OrdersProvider.GetAllOrders(filters.NotReadyFilterChecked, filters.NameFilter);
				}
				else
				{
					throw new ArgumentException("Wrong parameter passed to LoadOrdersCommand");
				}
				await Task.Delay(2000);

				foreach (var order in orders)
				{
					var orderViewModel = new OrderViewModel(order);
					_orders.Add(orderViewModel);
				}
				_logger.LogInformation("Orders have been loaded successfully.");
			}
			catch (Exception e)
			{
				_messageStore.SetMessage(e.Message, MessageType.Error);
				_logger.LogError(e.GetExceptionData());
			}
		}
	}
}
