using Models.Models;

namespace DataAccess.Abstractions
{
	public interface IOrderCreator
	{
		Task CreateOrder(IEnumerable<CartItem> chosenDishes);
	}
}
