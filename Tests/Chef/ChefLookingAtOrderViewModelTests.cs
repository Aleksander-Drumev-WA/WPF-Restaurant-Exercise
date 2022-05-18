namespace Tests.Chef;
public class ChefLookingAtOrderViewModelTests
{
	private IEnumerable<Dish> _dishes;
	private Order _order;
	private Mock<IDishProvider> dishProvider;
	private Mock<IOrderCreator> orderCreator;
	private Mock<IOrderProvider> orderProvider;
	private Mock<ILoggerFactory> _loggerFactoryMock;
	private Mock<IMessageStore> _messageStoreMock;

	[SetUp]
	public void SetUp()
	{
		_dishes = new List<Dish>()
			{
				new Dish(1, "Pizza", "path", "whatever", "ing. 1", "ing. 2", "ing. 3", "ing. 4"),
				new Dish(2, "Burger", "path", "whatever", "ing. 1", "ing. 2", "ing. 3", "ing. 4"),
				new Dish(3, "Beer", "path", "whatever", "ing. 1", "ing. 2", "ing. 3", "ing. 4"),
				new Dish(4, "Cake", "path", "whatever", "ing. 1", "ing. 2", "ing. 3", "ing. 4")
			};
		_order = new Order(_dishes, DateTime.UtcNow, 3);

		orderProvider = new Mock<IOrderProvider>();
		orderCreator = new Mock<IOrderCreator>();
		dishProvider = new Mock<IDishProvider>();

		_loggerFactoryMock = new Mock<ILoggerFactory>();
		_loggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>()))
			.Returns(new StubLogger());

		_messageStoreMock = new Mock<IMessageStore>();
	}

	[Test]
	public void OrderNumber_value_set_successfully()
	{
		// Arrange
		var sut = new ChefLookingAtOrderViewModel(_order, null);

		// Act

		// Assert
		sut.Should().NotBeNull();
		sut.OrderNumber.Should().Be(3);
	}

	[Test]
	public void RenderItem_equals_to_passed_dish()
	{
		// Arrange
		var sut = new ChefLookingAtOrderViewModel(_order, null);
		var firstDish = _dishes.First();
		var firstRenderItem = sut.RenderItems.First();

		// Act

		// Assert
		sut.Should().NotBeNull();
		sut.RenderItems.Count.Should().Be(4);
		_dishes.Count().Should().Be(4);
		firstDish.Should().BeEquivalentTo(firstRenderItem, options => options.Excluding(s => s.IsCompleted).ExcludingMissingMembers());
		firstDish.IsCompleted.Should().NotBe(firstRenderItem.IsCompleted);
	}

	[Test]
	public void CompleteDishCommand_triggers_LoadOrdersCommand_and_shows_orders_without_the_completed_one()
	{
		// Arrange
		var navigateCommandMock = new Mock<ICommand>();
		var messageStore = new MessageStore();
		var messageViewModel = new MessageViewModel(messageStore);
		_order.Dishes.RemoveAt(0);
		orderProvider.Setup(x => x.GetAllOrders(false, null)).ReturnsAsync(new List<Order>
			{
				_order
			});
		var restaurant = new Restaurant("Resty", dishProvider.Object, orderCreator.Object, orderProvider.Object);
		var mainChefViewModel = new MainChefViewModel(navigateCommandMock.Object, restaurant, messageStore, messageViewModel, _loggerFactoryMock.Object);
		var sut = new ChefLookingAtOrderViewModel(_order, mainChefViewModel.CompleteDishCommand);
		var chosenDish = new ChefLookingAtOrderItemViewModel("Pizza", "whatever", false, 1, 1);

		// Act
		sut.CompleteDishCommand.Execute(chosenDish);

		// Assert
		Assert.That(sut, Is.Not.Null);
		mainChefViewModel.Orders.Should().BeEquivalentTo(orderProvider.Object.GetAllOrders(false, null).Result, options => options.ExcludingMissingMembers());
	}
}