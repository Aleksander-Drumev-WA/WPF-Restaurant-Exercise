using Models.Models;
using WPF_Restaurant.Stores;

namespace Tests.Main;
public class MenuAndBasketViewModelTests
{
	private Restaurant _restaurant;
	private IEnumerable<Dish> _dishes;
	private Mock<IDishProvider> _dishProvider;
	private Mock<IOrderCreator> _orderCreator;
	private Mock<IOrderProvider> _orderProvider;
	private Mock<ILoggerFactory> _loggerFactoryMock;
	private MessageStore _messageStore;
	private Mock<ICommand> _navigateCommandMock;
	private MessageViewModel _messageViewModel;

	[SetUp]
	public void SetUp()
	{
		_dishes = new List<Dish>()
			{
				new Dish(1, "Pizza", "path", "whatever", true, "ing. 1", "ing. 2", "ing. 3", "ing. 4"),
				new Dish(2, "Burger", "path", "whatever", false, "ing. 1", "ing. 2", "ing. 3", "ing. 4"),
				new Dish(3, "Beer", "path", "whatever", true, "ing. 1", "ing. 2", "ing. 3", "ing. 4"),
				new Dish(4, "Cake", "path", "whatever", false, "ing. 1", "ing. 2", "ing. 3", "ing. 4")
			};

		_orderProvider = new Mock<IOrderProvider>();
		_orderCreator = new Mock<IOrderCreator>();
		_dishProvider = new Mock<IDishProvider>();

		_dishProvider.Setup(x => x.GetAllDishes()).ReturnsAsync(_dishes);

		_restaurant = new Restaurant("Resty", _dishProvider.Object, _orderCreator.Object, _orderProvider.Object);

		_loggerFactoryMock = new Mock<ILoggerFactory>();
		_loggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>()))
			.Returns(new StubLogger());

		_messageStore = new MessageStore();
		_navigateCommandMock = new Mock<ICommand>();
		_messageViewModel = new MessageViewModel(_messageStore);
	}

	[Test]
	public void Load_dishes_in_menu_successfully()
	{
		// Arrange
		var sut = new MenuAndBasketViewModel(_restaurant, _navigateCommandMock.Object, _messageStore, _messageViewModel, _loggerFactoryMock.Object);

		// Act

		// Assert
		sut.DishesInMenu.Should().NotBeEmpty();
		sut.DishesInMenu.Should().NotBeNull();
		sut.DishesInMenu.Should().BeEquivalentTo(_dishes, options => options.ExcludingMissingMembers());
	}

	[Test]
	public void Choose_dish_from_menu_successfully()
	{
		// Arrange
		var sut = new MenuAndBasketViewModel(_restaurant, _navigateCommandMock.Object, _messageStore, _messageViewModel, _loggerFactoryMock.Object);
		var resultList = new List<Dish>
			{
				_dishes.First(),
				_dishes.Last()
			};

		// Act
		sut.ChooseDishCommand.Execute(_dishes.First());
		sut.ChooseDishCommand.Execute(_dishes.Last());

		// Assert
		sut.ChosenDishes.Should().NotBeEmpty();
		sut.ChosenDishes.Should().NotBeNull();
		sut.ChosenDishes.Should().BeEquivalentTo(resultList, options => options.ExcludingMissingMembers());
	}

	[Test]
	public void Increase_quantity_of_chosen_dishes_successfully()
	{
		// Arrange
		var sut = new MenuAndBasketViewModel(_restaurant, _navigateCommandMock.Object, _messageStore, _messageViewModel, _loggerFactoryMock.Object);

		// Act
		sut.ChooseDishCommand.Execute(_dishes.First());
		sut.ChooseDishCommand.Execute(_dishes.Last());

		sut.IncreaseQuantityCommand.Execute(sut.ChosenDishes.First());
		sut.IncreaseQuantityCommand.Execute(sut.ChosenDishes.Last());


		// Assert
		sut.ChosenDishes.Should().NotBeEmpty();
		sut.ChosenDishes.Should().NotBeNull();
		sut.ChosenDishes.First().Quantity.Should().Be(2);
		sut.ChosenDishes.Last().Quantity.Should().Be(2);
	}

	[Test]
	public void Decrease_quantity_of_chosen_dishes_successfully()
	{
		// Arrange
		var sut = new MenuAndBasketViewModel(_restaurant, _navigateCommandMock.Object, _messageStore, _messageViewModel, _loggerFactoryMock.Object);

		// Act
		sut.ChooseDishCommand.Execute(_dishes.First());
		sut.ChooseDishCommand.Execute(_dishes.Last());

		sut.IncreaseQuantityCommand.Execute(sut.ChosenDishes.First());
		sut.IncreaseQuantityCommand.Execute(sut.ChosenDishes.Last());

		sut.DecreaseQuantityCommand.Execute(sut.ChosenDishes.First());
		sut.DecreaseQuantityCommand.Execute(sut.ChosenDishes.Last());


		// Assert
		sut.ChosenDishes.Should().NotBeEmpty();
		sut.ChosenDishes.Should().NotBeNull();
		sut.ChosenDishes.First().Quantity.Should().Be(1);
		sut.ChosenDishes.Last().Quantity.Should().Be(1);
	}

	[Test]
	public void Decrease_quantity_does_not_work_if_quantity_is_default_value()
	{
		// Arrange
		var sut = new MenuAndBasketViewModel(_restaurant, _navigateCommandMock.Object, _messageStore, _messageViewModel, _loggerFactoryMock.Object);

		// Act
		sut.ChooseDishCommand.Execute(_dishes.First());
		sut.ChooseDishCommand.Execute(_dishes.Last());

		sut.DecreaseQuantityCommand.Execute(sut.ChosenDishes.First());
		sut.DecreaseQuantityCommand.Execute(sut.ChosenDishes.Last());


		// Assert
		sut.ChosenDishes.Should().NotBeEmpty();
		sut.ChosenDishes.Should().NotBeNull();
		sut.ChosenDishes.First().Quantity.Should().Be(1);
		sut.ChosenDishes.Last().Quantity.Should().Be(1);
	}

	[Test]
	public void Navigate_to_other_VM()
	{
		//Arrange
		var navStore = new NavigationStore();
		var navigateCommand = new NavigateCommand<MainChefViewModel>(
			navStore,
			() => new MainChefViewModel(_navigateCommandMock.Object, _restaurant, _messageStore, _messageViewModel, _loggerFactoryMock.Object));

		var sut = new MenuAndBasketViewModel(_restaurant, navigateCommand, _messageStore, _messageViewModel, _loggerFactoryMock.Object);

		// Act
		sut.NavigateCommand.Execute(null);

		// Assert
		navStore.CurrentViewModel.Should().BeOfType<MainChefViewModel>();
	}

	[Test]
	public void Creating_order_from_chosen_dishes_successfully()
	{
		// Arrange
		var sut = new MenuAndBasketViewModel(_restaurant, _navigateCommandMock.Object, _messageStore, _messageViewModel, _loggerFactoryMock.Object);

		_orderCreator.Setup(x => x.CreateOrder(sut.ChosenDishes.Select(d => new CartItem(d.Dish, d.Quantity)))).ReturnsAsync(1);

		// Act
		sut.ChooseDishCommand.Execute(_dishes.First());
		sut.ChooseDishCommand.Execute(_dishes.Last());

		sut.OrderCommand.Execute(null);

		// Assert
		var resultOrder = new Order(sut.ChosenDishes.Select(d => d.Dish), DateTime.UtcNow, 1);
		resultOrder.Should().NotBeNull();
		resultOrder.Dishes.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(2).And.BeEquivalentTo(sut.ChosenDishes, options => options.ExcludingMissingMembers());


	}
}