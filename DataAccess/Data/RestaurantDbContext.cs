using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Restaurant.DataAccess.Entities;

namespace WPF_Restaurant.DataAccess.Data
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<DishEntity> Dishes { get; set; }

        public DbSet<OrderItemEntity> OrderItems { get; set; }

        public DbSet<OrderEntity> Orders { get; set; }
    }
}
