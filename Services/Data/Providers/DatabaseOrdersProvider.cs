using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Restaurant.Models;

namespace WPF_Restaurant.Services.Data.Providers
{
    public class DatabaseOrdersProvider
    {
        private readonly RestaurantDbContextFactory _dbContextFactory;

        public DatabaseOrdersProvider(RestaurantDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var orders = new List<Order>();
                var ordersDTO = dbContext.Orders.Include(o => o.OrderItems).ToList();

                foreach (var orderFromDb in ordersDTO)
                {
                    var dishes = orderFromDb.OrderItems.Select(oi => new Dish(oi.Dish.Name, oi.Dish.ImagePath, oi.Dish.Recipe, oi.Dish.Ingredients));

                    orders.Add(new Order(dishes, orderFromDb.CreatedOn, orderFromDb.Id));
                }

                return orders;
            }
        }
    }
}
