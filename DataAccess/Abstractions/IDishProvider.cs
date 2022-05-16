namespace DataAccess.Abstractions;
public interface IDishProvider
{
	Task<IEnumerable<Dish>> GetAllDishes();
}