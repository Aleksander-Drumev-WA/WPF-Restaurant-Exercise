namespace Tests.Chef;
public class ChefLookingAtRecipeViewModelTests
{
	private OrderItemViewModel _orderItemViewModel;
	private Order _order;
	private Mock<IDishProvider> _dishProvider;
	private Mock<IOrderCreator> _orderCreator;
	private Mock<IOrderProvider> _orderProvider;
	private Mock<ILoggerFactory> _loggerFactoryMock;
	private Mock<IMessageStore> _messageStoreMock;

	[SetUp]
	public void SetUp()
	{
		IEnumerable<Dish> dishes = new List<Dish>()
			{
				new Dish(1, "Pizza", "path", "whatever", false, "ing. 1", "ing. 2", "ing. 3", "ing. 4"),
				new Dish(1, "Pizza", "path", "whatever", false, "ing. 1", "ing. 2", "ing. 3", "ing. 4"),
				new Dish(1, "Pizza", "path", "whatever", true, "ing. 1", "ing. 2", "ing. 3", "ing. 4"),
				new Dish(1, "Pizza", "path", "whatever", true, "ing. 1", "ing. 2", "ing. 3", "ing. 4"),
			};
		_order = new Order(dishes, DateTime.UtcNow, 3);

		_orderProvider = new Mock<IOrderProvider>();
		_orderCreator = new Mock<IOrderCreator>();
		_dishProvider = new Mock<IDishProvider>();

		_orderItemViewModel = new OrderItemViewModel(dishes.First(), 4, 3, dishes.Select(d => d.IsCompleted));

		_loggerFactoryMock = new Mock<ILoggerFactory>();
		_loggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>()))
			.Returns(new StubLogger());

		_messageStoreMock = new Mock<IMessageStore>();
	}


	[Test]
	public void VM_properties_assigned_properly()
	{
		// Arrange
		var restaurant = new Restaurant("Resty", _dishProvider.Object, _orderCreator.Object, _orderProvider.Object);
		var sut = new ChefLookingAtRecipeViewModel(_orderItemViewModel, null);

		// Act

		// Assert
		sut.Should().NotBeNull();
		sut.DishId.Should().Be(1);
		sut.OrderNumber.Should().Be(3);
		sut.DishName.Should().Be("Pizza");
		sut.DishRecipe.Should().Be("whatever");
	}

	[Test]
	public void Dish_completion_changes_IsCompletedCollection()
	{
		// Arrange
		var navigateCommandMock = new Mock<ICommand>();
		var messageStore = new MessageStore();
		var messageViewModel = new MessageViewModel(messageStore);
		_orderProvider.Setup(x => x.GetAllOrders(false, null)).ReturnsAsync(new List<Order>
			{
				_order
			});
		_orderProvider.Setup(x => x.CompleteDish(1, 3)).ReturnsAsync(_order.Dishes.First().IsCompleted = true);
		var restaurant = new Restaurant("Resty", _dishProvider.Object, _orderCreator.Object, _orderProvider.Object);
		var mainViewModel = new MainChefViewModel(navigateCommandMock.Object, restaurant, messageStore, messageViewModel, _loggerFactoryMock.Object);
		var sut = new ChefLookingAtRecipeViewModel(_orderItemViewModel, mainViewModel.CompleteDishCommand);

		// Act
		sut.CompleteDishCommand.Execute(sut);

		// Assert
		mainViewModel.Should().NotBeNull();
		sut.Should().NotBeNull();
		mainViewModel.Orders.First().OrderItems.Count().Should().Be(_order.Dishes.Where(d => d.IsCompleted == false).Count());
		_order.Dishes.First().Should().BeEquivalentTo(mainViewModel.Orders.First().OrderItems.First(), options => options.ExcludingMissingMembers());
	}
}