global using FluentAssertions;
global using Moq;
global using NUnit.Framework;
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using WPF_Restaurant.Commands;
global using WPF_Restaurant.DataAccess.Data;
global using WPF_Restaurant.Models;
global using WPF_Restaurant.ViewModels;
global using WPF_Restaurant.ViewModels.Chef;
global using DataAccess.Abstractions;
global using Microsoft.Extensions.Logging;
global using Tests.Stubs;
global using System.Windows.Input;
global using WPF_Restaurant.Stores;
global using WPF_Restaurant.ViewModels.Common;

namespace Tests.Main;
public class MainChefViewModelTests
{
	private Order _order;
	private Restaurant _restaurant;
	private Mock<IDishProvider> _dishProvider;
	private Mock<IOrderCreator> _orderCreator;
	private Mock<IOrderProvider> _orderProvider;
	private Mock<ILoggerFactory> _loggerFactoryMock;
	private Mock<ICommand> _navigateCommandMock;
	private MessageStore _messageStore;
	private MessageViewModel _messageViewModel;


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

		_loggerFactoryMock = new Mock<ILoggerFactory>();
		_loggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>()))
			.Returns(new StubLogger());
		_navigateCommandMock = new Mock<ICommand>();
		_messageStore = new MessageStore();
		_messageViewModel = new MessageViewModel(_messageStore);
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
		var sut = new MainChefViewModel(_navigateCommandMock.Object, restaurant, _messageStore, _messageViewModel, _loggerFactoryMock.Object);

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
			() => new MenuAndBasketViewModel(_restaurant, _navigateCommandMock.Object, _messageStore, _messageViewModel, _loggerFactoryMock.Object));

		var sut = new MainChefViewModel(navigateCommand, _restaurant, _messageStore, _messageViewModel, _loggerFactoryMock.Object);

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
		var sut = new MainChefViewModel(_navigateCommandMock.Object, _restaurant, _messageStore, _messageViewModel, _loggerFactoryMock.Object);
		var orderItemViewModel = new OrderItemViewModel(_order.Dishes.First(), 1, 3, new List<bool> { false });

		// Act
		sut.NavigateToRecipeViewCommand.Execute(orderItemViewModel);

		// Assert
		sut.CurrentViewModel.Should().BeOfType<ChefLookingAtRecipeViewModel>();
	}

	[Test]
	public void Cannot_show_recipe_because_no_dish_is_provided()
	{
		// Arrange
		_orderProvider.Setup(x => x.GetAllOrders(false, null)).ReturnsAsync(new List<Order>
			{
				_order
			});
		var sut = new MainChefViewModel(_navigateCommandMock.Object, _restaurant, _messageStore, _messageViewModel, _loggerFactoryMock.Object);

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
		var sut = new MainChefViewModel(_navigateCommandMock.Object, _restaurant, _messageStore, _messageViewModel, _loggerFactoryMock.Object);

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
		var sut = new MainChefViewModel(_navigateCommandMock.Object, _restaurant, _messageStore, _messageViewModel, _loggerFactoryMock.Object);

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
		var sut = new MainChefViewModel(_navigateCommandMock.Object, restaurant, _messageStore, _messageViewModel, _loggerFactoryMock.Object);

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
		var sut = new MainChefViewModel(_navigateCommandMock.Object, restaurant, _messageStore, _messageViewModel, _loggerFactoryMock.Object);

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
		var sut = new MainChefViewModel(_navigateCommandMock.Object, restaurant, _messageStore, _messageViewModel, _loggerFactoryMock.Object);

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
}