using WPF_Restaurant.Models;

namespace Models.Models;
public class CartItem
{
	private readonly Dish _dish;
	private int _quantity;

	public Dish Dish => _dish;

	public int Quantity => _quantity;

	public CartItem(Dish dish, int quantity)
	{
		_dish = dish;
		_quantity = quantity;
	}
}