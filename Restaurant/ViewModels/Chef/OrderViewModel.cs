using WPF_Restaurant.Resources.Comparers;

namespace WPF_Restaurant.ViewModels.Chef;

public class OrderViewModel : BaseViewModel
{
	private readonly Order _order;

	public Order Order => _order;

	public int OrderNumber => _order.Id;

	public IEnumerable<OrderItemViewModel> OrderItems => _order.Dishes
														 .GroupBy(d => d, new DishEqualityComparer())
														 .Select(d =>
														 new OrderItemViewModel(d.Key,
																				d.Count(),
																				OrderNumber,
																				d.Where(x => x.IsCompleted == false).Select(x => x.IsCompleted)));

	public OrderViewModel(Order order)
	{
		_order = order;
	}
}