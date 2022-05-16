namespace WPF_Restaurant.Models;
public class Order
{
	private readonly List<Dish> _dishes;
	private readonly DateTime _createdOn;
	private readonly int _id;

	public int Id => _id;

	public List<Dish> Dishes => _dishes;

	public DateTime CreatedOn => _createdOn;

	public Order(IEnumerable<Dish> dishes, DateTime createdOn, int id)
	{
		_dishes = new List<Dish>();
		_dishes.AddRange(dishes);

		_createdOn = createdOn;
		_id = id;
	}
}