namespace Tests.Chef;
public class OrderViewModelTests
{
	[Test]
	public void VM_properties_are_set_properly()
	{
		// Arrange
		var dishes = new List<Dish>()
			{
				new Dish(1, "Pizza", "path", "whatever", "ing. 1", "ing. 2", "ing. 3", "ing. 4"),
				new Dish(2, "Burger", "path", "whatever", "ing. 1", "ing. 2", "ing. 3", "ing. 4"),
				new Dish(3, "Beer", "path", "whatever", "ing. 1", "ing. 2", "ing. 3", "ing. 4"),
				new Dish(4, "Cake", "path", "whatever", "ing. 1", "ing. 2", "ing. 3", "ing. 4")
			};
		var order = new Order(dishes, DateTime.UtcNow, 3);
		var sut = new OrderViewModel(order);

		// Act

		// Assert
		sut.Order.Should().NotBeNull();
		sut.Order.Id.Should().Be(order.Id);
		sut.Order.CreatedOn.Should().Be(order.CreatedOn);
		sut.Order.Dishes.Should().BeEquivalentTo(order.Dishes);

		sut.OrderNumber.Should().Be(order.Id);

		sut.OrderItems.Count().Should().Be(order.Dishes.Count());
		sut.OrderItems.Should().BeEquivalentTo(order.Dishes, options => options.ExcludingMissingMembers());
	}
}