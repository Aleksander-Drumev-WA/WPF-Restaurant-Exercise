using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Restaurant.Models
{
    public class Kitchen
    {
        private readonly List<Order> _orders;

        public Kitchen()
        {
            _orders = new List<Order>();
        }

        public Order GetOrderByNumber(int number)
        {
            return _orders[number];
        }

        public void AddOrder(Order order)
        {
            _orders.Add(order);
        }

        public void CompleteOrder(Order order)
        {
            _orders.Remove(order);
        }
    }
}
