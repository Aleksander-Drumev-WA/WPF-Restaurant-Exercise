using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Restaurant.Models;
using WPF_Restaurant.ViewModels;

namespace WPF_Restaurant.Services.Data.Providers
{
    public class DatabaseDishProvider
    {
        private readonly RestaurantDbContextFactory _dbContextFactory;

        public DatabaseDishProvider(RestaurantDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<DishViewModel>> GetAllDishes()
        {
            using(var dbContext = _dbContextFactory.CreateDbContext())
            {
                var dishesDTO = await dbContext.Dishes.ToListAsync();

                var dishes = dishesDTO.Select(dto => new Dish(dto.Name, dto.ImagePath, dto.Recipe, dto.Ingredients));

                return dishes.Select(d => new DishViewModel(d));
            }
        }
    }
}
