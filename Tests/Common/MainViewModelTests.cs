using FluentAssertions;
using NUnit.Framework;

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
			var navStore = new NavigationStore();
			var sut = new MainViewModel(navStore);
			var viewModel = new MainChefViewModel(null, null, null, null, null);

			// Act
			sut.CurrentViewModel = viewModel;

			// Assert

			sut.CurrentViewModel.Should().BeOfType<MainChefViewModel>();
		}

	}
}
