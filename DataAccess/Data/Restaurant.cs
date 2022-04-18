using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Restaurant.DataAccess.Data.Providers;

namespace WPF_Restaurant.DataAccess.Data
{
    public class Restaurant
    {
        private readonly DatabaseDishProvider _dishProvider;
        private readonly DatabaseOrderCreator _databaseOrderCreator;
        private readonly DatabaseOrdersProvider _databaseOrdersProvider;

        public string Name { get; }

        public DatabaseDishProvider DishProvider => _dishProvider;

        public DatabaseOrderCreator OrderCreator => _databaseOrderCreator;

        public DatabaseOrdersProvider OrdersProvider => _databaseOrdersProvider;

        public Restaurant(
            string name,
            DatabaseDishProvider dishProvider,
            DatabaseOrderCreator databaseOrderCreator,
            DatabaseOrdersProvider databaseOrdersProvider)
        {
            Name = name;
            _dishProvider = dishProvider;
            _databaseOrderCreator = databaseOrderCreator;
            _databaseOrdersProvider = databaseOrdersProvider;
        }
    }
}
