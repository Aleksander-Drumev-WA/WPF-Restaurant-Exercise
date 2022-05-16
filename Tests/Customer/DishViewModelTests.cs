using WPF_Restaurant.ViewModels.Customer;

namespace Tests.Customer;
public class DishViewModelTests
{
	private Dish _dish;

	[SetUp]
	public void SetUp()
	{
		_dish = new Dish(1, "Pizza", "path", "whatever", "ing. 1", "ing. 2", "ing. 3", "ing. 4");
	}

	[Test]
	public void VM_properties_are_set_properly()
	{
		// Arrange
		var sut = new DishViewModel(_dish);

		// Act

		// Assert
		sut.Dish.Should().NotBeNull();
		sut.Name.Should().Be(_dish.Name);
		sut.ImagePath.Should().Be(_dish.ImagePath);
		sut.Recipe.Should().Be(_dish.Recipe);
		sut.Ingredients.Should().Be(_dish.Ingredients);
		sut.Quantity.Should().Be(1);
	}

	[Test]
	public void Changing_quantity_value_successfully()
	{
		// Arrange
		var sut = new DishViewModel(_dish);

		// Act
		sut.Quantity = 15;

		// Assert
		sut.Quantity.Should().Be(15);
	}
}