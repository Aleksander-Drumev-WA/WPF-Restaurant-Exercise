using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Restaurant.Models;

namespace WPF_Restaurant.DataAccess.Data.Providers
{
    public class DatabaseOrdersProvider
    {
        private readonly RestaurantDbContextFactory _dbContextFactory;

        public DatabaseOrdersProvider(RestaurantDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Order>> GetAllOrders(bool notReady = false, string? nameFilter = null)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var orders = new List<Order>();
                var ordersDTO = await dbContext.Orders
                    .Include(o => o.OrderItems
                    .Where(oi => (oi.IsCompleted == !notReady) && (nameFilter == null ? true : oi.Dish.Name.ToLower().Contains(nameFilter.ToLower()))))
                    .ThenInclude(x => x.Dish)
                    .ToListAsync();

                foreach (var orderFromDb in ordersDTO)
                {
                    var dishes = orderFromDb.OrderItems.Select(oi => new Dish(oi.DishId, oi.Dish.Name, oi.Dish.ImagePath, oi.Dish.Recipe, oi.IsCompleted, oi.Dish.Ingredients));

                    orders.Add(new Order(dishes, orderFromDb.CreatedOn, orderFromDb.Id));
                }

                return orders;
            }
        }

        public async Task CompleteDish(int dishId, int orderNumber)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var dishToChangeCompletedState = await dbContext.OrderItems.FirstOrDefaultAsync(oi => oi.DishId == dishId && oi.OrderId == orderNumber && oi.IsCompleted == false);
                if (dishToChangeCompletedState != null)
                {
                    dishToChangeCompletedState.IsCompleted = true;

                    dbContext.OrderItems.Update(dishToChangeCompletedState);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
