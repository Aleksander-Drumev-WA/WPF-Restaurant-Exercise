using WPF_Restaurant.Models;

namespace DataAccess.Abstractions
{
	public interface IDishProvider
	{
		Task<IEnumerable<Dish>> GetAllDishes();
	}
}
