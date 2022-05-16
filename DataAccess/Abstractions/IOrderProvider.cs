namespace DataAccess.Abstractions;
public interface IOrderProvider
{
	Task<IEnumerable<Order>> GetAllOrders(bool notReady = false, string? nameFilter = null);

	Task<bool> CompleteDish(int dishId, int orderNumber);
}