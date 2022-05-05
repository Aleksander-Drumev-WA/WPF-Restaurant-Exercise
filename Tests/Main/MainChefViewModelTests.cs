﻿using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

using DataAccess.Abstractions;
using WPF_Restaurant.Commands;
using WPF_Restaurant.DataAccess.Data;
using WPF_Restaurant.Models;
using WPF_Restaurant.Stores;
using WPF_Restaurant.ViewModels;
using WPF_Restaurant.ViewModels.Chef;
using Microsoft.Extensions.Logging;

namespace Tests.Main
{
	public class MainChefViewModelTests
	{
		private Order _order;
		private Restaurant _restaurant;
		private Mock<IDishProvider> _dishProvider;
		private Mock<IOrderCreator> _orderCreator;
		private Mock<IOrderProvider> _orderProvider;

		private Mock<ILoggerFactory> _logFactoryMock;
		private Mock<IMessageStore> _messageStoreMock;


		[SetUp]
		public void SetUp()
		{
			var dishes = new List<Dish>()
			{
				new Dish(1, "Pizza", "path", "whatever", true, "ing. 1", "ing. 2", "ing. 3", "ing. 4"),
				new Dish(2, "Burger", "path", "whatever", false, "ing. 1", "ing. 2", "ing. 3", "ing. 4"),
				new Dish(3, "Beer", "path", "whatever", true, "ing. 1", "ing. 2", "ing. 3", "ing. 4"),
				new Dish(4, "Cake", "path", "whatever", false, "ing. 1", "ing. 2", "ing. 3", "ing. 4")
			};
			_order = new Order(dishes, DateTime.UtcNow, 3);

			_orderProvider = new Mock<IOrderProvider>();
			_orderCreator = new Mock<IOrderCreator>();
			_dishProvider = new Mock<IDishProvider>();
			_restaurant = new Restaurant("Resty", _dishProvider.Object, _orderCreator.Object, _orderProvider.Object);

			// We need mock the CreateLogger because test try to write to logs.
			_logFactoryMock = new Mock<ILoggerFactory>();
			_logFactoryMock.Setup(m => m.CreateLogger(It.IsAny<string>()))
				.Returns(new StubLogger());

			// We don't mock _messageStoreMock here because don't test it
			_messageStoreMock = new Mock<IMessageStore>();
		}

		[Test]
		public void Initially_loading_orders_successfully()
		{
			// Arrange
			_orderProvider.Setup(x => x.GetAllOrders(false, null)).ReturnsAsync(new List<Order>
			{
				_order
			});
			var restaurant = new Restaurant("Resty", _dishProvider.Object, _orderCreator.Object, _orderProvider.Object);
			var sut = new MainChefViewModel(null, restaurant, null, null, null);

			// Act

			// Assert
			sut.Orders.Should().NotBeEmpty();
			sut.Orders.Should().NotBeNull();
			sut.Orders.First().OrderItems.Should().BeEquivalentTo(_order.Dishes, options => options.ExcludingMissingMembers());
		}

		[Test]
		public void Navigate_to_other_VM_successfully()
		{
			// Arrange
			_orderProvider.Setup(x => x.GetAllOrders(false, null)).ReturnsAsync(new List<Order>
			{
				_order
			});
			var navStore = new NavigationStore();

			var navigateCommand = new NavigateCommand<MenuAndBasketViewModel>(
				navStore, 
				() => new MenuAndBasketViewModel(_restaurant, null, _messageStoreMock.Object, null, _logFactoryMock.Object));

			var sut = new MainChefViewModel(navigateCommand, _restaurant, null, null, null);

			// Act
			sut.NavigateCommand.Execute(null);

			// Assert
			navStore.CurrentViewModel.Should().BeOfType<MenuAndBasketViewModel>();
		}

		[Test]
		public void Show_recipe_successfully()
		{
			// Arrange
			_orderProvider.Setup(x => x.GetAllOrders(false, null)).ReturnsAsync(new List<Order>
			{
				_order
			});
			var sut = new MainChefViewModel(null, _restaurant, null, null, null);
			var orderItemViewModel = new OrderItemViewModel(_order.Dishes.First(), 1, 3, new List<bool> { false });

			// Act
			sut.NavigateToRecipeViewCommand.Execute(orderItemViewModel);

			// Assert
			sut.CurrentViewModel.Should().BeOfType<ChefLookingAtRecipeViewModel>();
		}

		[Test]
		public void Cannot_show_recipe_because_dish_is_not_in_list()
		{
			// Arrange
			_orderProvider.Setup(x => x.GetAllOrders(false, null)).ReturnsAsync(new List<Order>
			{
				_order
			});
			var orderItemViewModel = new OrderItemViewModel(new Dish(3, "Some name", "path 2", "strong recipe", "salt"), 1, 5, new List<bool> { false });
			var sut = new MainChefViewModel(null, _restaurant, null, null, null);

			// Act
			sut.NavigateToRecipeViewCommand.Execute(orderItemViewModel);

			// Assert
			sut.CurrentViewModel.Should().BeNull();
		}

		[Test]
		public void Cannot_show_recipe_because_no_dish_is_provided()
		{
			// Arrange
			_orderProvider.Setup(x => x.GetAllOrders(false, null)).ReturnsAsync(new List<Order>
			{
				_order
			});
			var sut = new MainChefViewModel(null, _restaurant, null, null, null);

			// Act
			sut.NavigateToRecipeViewCommand.Execute(null);

			// Assert
			sut.CurrentViewModel.Should().BeNull();
		}

		[Test]
		public void Show_dishes_in_order_successfully()
		{
			// Arrange
			_orderProvider.Setup(x => x.GetAllOrders(false, null)).ReturnsAsync(new List<Order>
			{
				_order
			});
			var sut = new MainChefViewModel(null, _restaurant, null, null, null);

			// Act
			sut.ShowDishesInOrderCommand.Execute(sut.Orders.First().OrderNumber);

			// Assert
			sut.CurrentViewModel.Should().BeOfType<ChefLookingAtOrderViewModel>();
		}

		[Test]
		public void Dishes_in_order_not_found_because_no_such_OrderNumber_exists()
		{
			// Arrange
			_orderProvider.Setup(x => x.GetAllOrders(false, null)).ReturnsAsync(new List<Order>
			{
				_order
			});
			var sut = new MainChefViewModel(null, _restaurant, null, null, null);

			// Act
			sut.ShowDishesInOrderCommand.Execute("100");

			// Assert
			sut.CurrentViewModel.Should().BeNull();
		}

		[Test]
		public void Loading_orders_with_NotReady_filter_being_checked()
		{
			// Arrange
			var dishes = new List<Dish>()
			{
				new Dish(1, "Pizza", "path", "whatever", true, "ing. 1", "ing. 2", "ing. 3", "ing. 4"),
				new Dish(3, "Beer", "path", "whatever", true, "ing. 1", "ing. 2", "ing. 3", "ing. 4")
			};
			_order = new Order(dishes, DateTime.UtcNow, 3);
			_orderProvider.Setup(x => x.GetAllOrders(true, null)).ReturnsAsync(new List<Order>
			{
				_order
			});
			var restaurant = new Restaurant("Resty", _dishProvider.Object, _orderCreator.Object, _orderProvider.Object);
			var sut = new MainChefViewModel(null, restaurant, null, null, null);

			// Act
			sut.NotReadyFilterChecked = true;
			sut.LoadOrdersCommand.Execute(sut);

			// Assert
			sut.Orders.Should().NotBeEmpty();
			sut.Orders.Should().NotBeNull();
			sut.Orders.First().OrderItems.Count().Should().Be(_order.Dishes.Count());
			sut.Orders.First().OrderItems
				.Should()
				.BeEquivalentTo(_order.Dishes, options => options.ExcludingMissingMembers());
		}

		[Test]
		public void Loading_orders_with_NameFilter()
		{
			// Arrange
			var dishes = new List<Dish>()
			{
				new Dish(1, "Pizza", "path", "whatever", false, "ing. 1", "ing. 2", "ing. 3", "ing. 4")
			};
			_order = new Order(dishes, DateTime.UtcNow, 3);
			_orderProvider.Setup(x => x.GetAllOrders(false, "piz")).ReturnsAsync(new List<Order>
			{
				_order
			});
			var restaurant = new Restaurant("Resty", _dishProvider.Object, _orderCreator.Object, _orderProvider.Object);
			var sut = new MainChefViewModel(null, restaurant, null, null, null);

			// Act
			sut.NameFilter = "piz";
			sut.LoadOrdersCommand.Execute(sut);

			// Assert
			sut.Orders.Should().NotBeEmpty();
			sut.Orders.Should().NotBeNull();
			sut.Orders.First().OrderItems.Count().Should().Be(_order.Dishes.Count());
			sut.Orders.First().OrderItems
				.Should()
				.BeEquivalentTo(_order.Dishes, options => options.ExcludingMissingMembers());
		}

		[Test]
		public void Loading_order_with_both_filters()
		{
			// Arrange
			var dishes = new List<Dish>()
			{
				new Dish(1, "Pizza", "path", "whatever", true, "ing. 1", "ing. 2", "ing. 3", "ing. 4")
			};
			_order = new Order(dishes, DateTime.UtcNow, 3);
			_orderProvider.Setup(x => x.GetAllOrders(true, "piz")).ReturnsAsync(new List<Order>
			{
				_order
			});
			var restaurant = new Restaurant("Resty", _dishProvider.Object, _orderCreator.Object, _orderProvider.Object);
			var sut = new MainChefViewModel(null, restaurant, null, null, null);

			// Act
			sut.NameFilter = "piz";
			sut.NotReadyFilterChecked = true;
			sut.LoadOrdersCommand.Execute(sut);

			// Assert
			sut.Orders.Should().NotBeEmpty();
			sut.Orders.Should().NotBeNull();
			sut.Orders.First().OrderItems.Count().Should().Be(_order.Dishes.Count());
			sut.Orders.First().OrderItems
				.Should()
				.BeEquivalentTo(_order.Dishes, options => options.ExcludingMissingMembers());
		}

		internal class StubLogger : ILogger
		{
			public IDisposable BeginScope<TState>(TState state)
			{
				return null;
			}

			public bool IsEnabled(LogLevel logLevel)
			{
				return false;
			}

			public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
			{
			}
		}
	}
}