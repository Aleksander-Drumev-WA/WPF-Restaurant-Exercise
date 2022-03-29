using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Restaurant.DTOs;
using WPF_Restaurant.Models;

namespace WPF_Restaurant.Services.Data.Providers
{
    public class DatabaseOrderCreator
    {
        private readonly RestaurantDbContextFactory _dbContextFactory;

        public DatabaseOrderCreator(RestaurantDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<int> CreateOrder()
        {
            using(var dbContext = _dbContextFactory.CreateDbContext())
            {
                var order = new OrderDTO();
                dbContext.Orders.Add(order);
                await dbContext.SaveChangesAsync();
                
                return order.Id;
            }
        }

        public async Task PopulateOrder(IEnumerable<DishInOrderDTO> dishes)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                dbContext.DishesInOrder.AddRange(dishes);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
