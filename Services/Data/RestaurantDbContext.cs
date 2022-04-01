using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Restaurant.DTOs;

namespace WPF_Restaurant.Services.Data
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<DishDTO> Dishes { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<OrderDTO> Orders { get; set; }
    }
}
