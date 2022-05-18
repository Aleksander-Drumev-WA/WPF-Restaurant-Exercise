namespace Tests.Common;
public class MainViewModelTests
{
	[Test]
	public void Successfully_changing_CurrentViewModel()
	{
		// Arrange
		var navigateCommandMock = new Mock<ICommand>();
		var messageStore = new MessageStore();
		var messageViewModel = new MessageViewModel(messageStore);
		var dishProvider = new Mock<IDishProvider>();
		var orderCreator = new Mock<IOrderCreator>();
		var orderProvider = new Mock<IOrderProvider>();
		var restaurant = new Restaurant("Resty", dishProvider.Object, orderCreator.Object, orderProvider.Object);

		var loggerMock = new Mock<ILoggerFactory>();

		loggerMock.Setup(x => x.CreateLogger(It.IsAny<string>()))
			.Returns(new StubLogger());

		var navStore = new NavigationStore();
		var sut = new MainViewModel(navStore);
		var viewModel = new MainChefViewModel(navigateCommandMock.Object, restaurant, messageStore, messageViewModel, loggerMock.Object);

		// Act
		sut.CurrentViewModel = viewModel;

		// Assert

		sut.CurrentViewModel.Should().BeOfType<MainChefViewModel>();
	}

}