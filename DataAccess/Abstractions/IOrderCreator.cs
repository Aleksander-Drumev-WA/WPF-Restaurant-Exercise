using Models.Models;

namespace DataAccess.Abstractions;
public interface IOrderCreator
{
	Task<int> CreateOrder(IEnumerable<CartItem> chosenDishes);
}