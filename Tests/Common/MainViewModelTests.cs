using DataAccess.Abstractions;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Tests.Stubs;
using WPF_Restaurant.DataAccess.Data;
using WPF_Restaurant.Stores;
using WPF_Restaurant.ViewModels;
using WPF_Restaurant.ViewModels.Common;

namespace Tests.Common
{
	public class MainViewModelTests
	{
		[Test]
		public void Successfully_changing_CurrentViewModel()
		{
			// Arrange
			var dishProvider = new Mock<IDishProvider>();
			var orderCreator = new Mock<IOrderCreator>();
			var orderProvider = new Mock<IOrderProvider>();
			var restaurant = new Restaurant("Resty", dishProvider.Object, orderCreator.Object, orderProvider.Object);

			var loggerMock = new Mock<ILoggerFactory>();

			loggerMock.Setup(x => x.CreateLogger(It.IsAny<string>()))
				.Returns(new StubLogger());

			var navStore = new NavigationStore();
			var sut = new MainViewModel(navStore);
			var viewModel = new MainChefViewModel(null, restaurant, null, null, loggerMock.Object);

			// Act
			sut.CurrentViewModel = viewModel;

			// Assert

			sut.CurrentViewModel.Should().BeOfType<MainChefViewModel>();
		}

	}
}
