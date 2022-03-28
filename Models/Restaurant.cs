using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Restaurant.Services.Data.Providers;

namespace WPF_Restaurant.Models
{
    public class Restaurant
    {
        private readonly Kitchen _kitchen;
        private readonly DatabaseDishProvider _dishProvider;

        public string Name { get; }

        public DatabaseDishProvider DishProvider => _dishProvider;

        public Restaurant(string name, DatabaseDishProvider dishProvider)
        {
            Name = name;
            _dishProvider = dishProvider;
            _kitchen = new Kitchen();
        }

        public Order GetOrderByNumber(int number)
        {
            return _kitchen.GetOrderByNumber(number);
        }

        public void CreateOrder(Order order)
        {
            _kitchen.AddOrder(order);
        }
    }
}
