using DataAccess.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Restaurant.Models;

namespace WPF_Restaurant.DataAccess.Data.Providers
{
    public class DatabaseDishProvider : IDishProvider
    {
        private readonly RestaurantDbContextFactory _dbContextFactory;

        public DatabaseDishProvider(RestaurantDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Dish>> GetAllDishes()
        {
            using(var dbContext = _dbContextFactory.CreateDbContext())
            {
                var dishesDTO = await dbContext.Dishes.ToListAsync();

                return dishesDTO.Select(dto => new Dish(dto.Id, dto.Name, dto.ImagePath, dto.Recipe, dto.Ingredients));
            }
        }
    }
}
