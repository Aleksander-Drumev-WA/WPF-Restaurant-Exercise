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
        private readonly DatabaseOrderCreator _databaseOrderCreator;

        public string Name { get; }

        public DatabaseDishProvider DishProvider => _dishProvider;

        public DatabaseOrderCreator OrderCreator => _databaseOrderCreator;

        public Restaurant(string name, DatabaseDishProvider dishProvider, DatabaseOrderCreator databaseOrderCreator)
        {
            Name = name;
            _dishProvider = dishProvider;
            _databaseOrderCreator = databaseOrderCreator;
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
