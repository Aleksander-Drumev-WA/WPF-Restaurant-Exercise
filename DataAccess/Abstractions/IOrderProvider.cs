﻿using WPF_Restaurant.Models;

namespace DataAccess.Abstractions
{
	public interface IOrderProvider
	{
		Task<IEnumerable<Order>> GetAllOrders(bool notReady = false, string? nameFilter = null);

		Task CompleteDish(int dishId, int orderNumber);
	}
}