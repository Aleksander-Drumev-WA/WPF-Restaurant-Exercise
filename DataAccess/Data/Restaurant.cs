using DataAccess.Abstractions;
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
        private readonly IDishProvider _dishProvider;
        private readonly IOrderCreator _databaseOrderCreator;
        private readonly IOrderProvider _databaseOrdersProvider;

        public string Name { get; }

        public IDishProvider DishProvider => _dishProvider;

        public IOrderCreator OrderCreator => _databaseOrderCreator;

        public IOrderProvider OrdersProvider => _databaseOrdersProvider;

        public Restaurant(
            string name,
            IDishProvider dishProvider,
            IOrderCreator databaseOrderCreator,
            IOrderProvider databaseOrdersProvider)
        {
            Name = name;
            _dishProvider = dishProvider;
            _databaseOrderCreator = databaseOrderCreator;
            _databaseOrdersProvider = databaseOrdersProvider;
        }
    }
}
