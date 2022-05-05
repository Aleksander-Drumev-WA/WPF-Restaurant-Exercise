using DataAccess.Abstractions;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Restaurant.DataAccess.Entities;
using WPF_Restaurant.Models;

namespace WPF_Restaurant.DataAccess.Data.Providers
{
    public class DatabaseOrderCreator : IOrderCreator
    {
        private readonly RestaurantDbContextFactory _dbContextFactory;

        public DatabaseOrderCreator(RestaurantDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<int> CreateOrder(IEnumerable<CartItem> chosenDishes)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var orderItems = new List<OrderItemEntity>();
                var order = new OrderEntity();
                dbContext.Orders.Add(order);
                await dbContext.SaveChangesAsync();

                var allDishes = dbContext.Dishes;

                foreach (var chosenDish in chosenDishes)
                {
                    var dish = allDishes.FirstOrDefault(d => d.Name == chosenDish.Dish.Name);
                    
                    for (int i = 0; i < chosenDish.Quantity; i++)
                    {
                        var orderItem = new OrderItemEntity
                        {
                            DishId = dish.Id,
                            OrderId = order.Id
                        };

                        orderItems.Add(orderItem);
                    }
                }

                dbContext.OrderItems.AddRange(orderItems);
                await dbContext.SaveChangesAsync();

                return order.Id;
            }
        }
    }
}
