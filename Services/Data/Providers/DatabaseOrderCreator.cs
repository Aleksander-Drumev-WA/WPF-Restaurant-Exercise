using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Restaurant.DTOs;
using WPF_Restaurant.Models;
using WPF_Restaurant.ViewModels;

namespace WPF_Restaurant.Services.Data.Providers
{
    public class DatabaseOrderCreator
    {
        private readonly RestaurantDbContextFactory _dbContextFactory;

        public DatabaseOrderCreator(RestaurantDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreateOrder(List<Dish> chosenDishes)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var orderItems = new List<OrderItemDTO>();
                var order = new OrderDTO();
                dbContext.Orders.Add(order);
                await dbContext.SaveChangesAsync();

                var allDishes = dbContext.Dishes;

                foreach (var chosenDish in chosenDishes)
                {
                    var dish = allDishes.FirstOrDefault(d => d.Name == chosenDish.Name);
                    
                    for (int i = 0; i < chosenDish.Quantity; i++)
                    {
                        var orderItem = new OrderItemDTO
                        {
                            DishId = dish.Id,
                            OrderId = order.Id
                        };

                        orderItems.Add(orderItem);
                    }
                }

                dbContext.OrderItems.AddRange(orderItems);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
