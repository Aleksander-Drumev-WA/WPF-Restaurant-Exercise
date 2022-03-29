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
                var order = new OrderDTO();
                dbContext.Orders.Add(order);
                await dbContext.SaveChangesAsync();

                var dishesForOrder = _chosenDishes.Select(cd => new DishInOrderDTO()
                {
                    Name = cd.Name,
                    Quantity = cd.Quantity,
                    IsReady = false,
                    OrderId = order.Id
                });

                dbContext.DishesInOrder.AddRange(dishesForOrder);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
