namespace Tests.Chef;
public class ChefLookingAtOrderItemViewModelTests
{
	[Test]
	public void Creation_is_successfull()
	{
		// Arrange
		var sut = new ChefLookingAtOrderItemViewModel("pizza", "whatever", false, 5, 2);

		// Act?

		// Assert
		sut.Should().NotBeNull();
		sut.Name.Should().Be("pizza");
		sut.Recipe.Should().Be("whatever");
		sut.IsCompleted.Should().BeFalse();
		sut.OrderNumber.Should().Be(5);
		sut.DishId.Should().Be(2);

	}

	[Test]
	public void Change_isCompleted_value()
	{
		// Arrange
		var sut = new ChefLookingAtOrderItemViewModel("pizza", "whatever", false, 5, 2);

		// Act
		sut.IsCompleted = true;

		// Assert
		sut.Should().NotBeNull();
		sut.Name.Should().Be("pizza");
		sut.Recipe.Should().Be("whatever");
		sut.IsCompleted.Should().BeTrue();
		sut.OrderNumber.Should().Be(5);
		sut.DishId.Should().Be(2);

	}
}