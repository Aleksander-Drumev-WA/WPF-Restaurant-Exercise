using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Restaurant.Models
{
    public class Restaurant
    {
        private readonly Kitchen _kitchen;

        public string Name { get; }

        public Restaurant(string name)
        {
            Name = name;
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
