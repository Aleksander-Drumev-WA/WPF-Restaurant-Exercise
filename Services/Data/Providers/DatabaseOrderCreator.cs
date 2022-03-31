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

        public async Task CreateOrder(ObservableCollection<DishViewModel> _chosenDishes)
        {
            using(var dbContext = _dbContextFactory.CreateDbContext())
            {
                var orderItems = new List<OrderItem>();
                var order = new OrderDTO();
                dbContext.Orders.Add(order);
                await dbContext.SaveChangesAsync();

                var allDishes = dbContext.Dishes;

                foreach (var chosenDish in _chosenDishes)
                {
                    var dish = allDishes.FirstOrDefault(d => d.Name == chosenDish.Name);

                    var orderItem = new OrderItem
                    {
                        DishId = dish.Id,
                        OrderId = order.Id
                    };

                    orderItems.Add(orderItem);
                }

                dbContext.OrderItems.AddRange(orderItems);
                await dbContext.SaveChangesAsync();
                _chosenDishes.Clear();
            }
        }
    }
}
