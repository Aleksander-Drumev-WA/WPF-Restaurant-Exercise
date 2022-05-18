namespace Tests.Chef;
public class OrderItemViewModelTests
{
	[Test]
	public void VM_properties_assigned_properly()
	{
		// Arrange
		var dish = new Dish(1, "Pizza", "path", "whatever", "ing. 1", "ing. 2", "ing. 3", "ing. 4");
		IEnumerable<bool> isCompletedCollection = new List<bool>()
			{
				true,
				false,
				true,
				false
			};

		var sut = new OrderItemViewModel(dish, 4, 3, isCompletedCollection);

		// Act

		// Assert
		sut.Id.Should().Be(dish.Id);
		sut.Recipe.Should().Be(dish.Recipe);
		sut.Name.Should().Be(dish.Name);
		sut.Quantity.Should().Be(4);
		sut.OrderNumber.Should().Be(3);
		sut.IsCompletedCollection.Should().BeEquivalentTo(isCompletedCollection);
		sut.RenderCount.Should().Be(4);
	}
}