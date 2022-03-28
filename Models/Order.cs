using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Restaurant.Models
{
    public class Order
    {
        private List<Dish> _dishes;

        public List<Dish> Dishes => _dishes;

        public Order(IEnumerable<Dish> dishes)
        {
            _dishes = new List<Dish>();
            _dishes.AddRange(dishes);
        }
    }
}
