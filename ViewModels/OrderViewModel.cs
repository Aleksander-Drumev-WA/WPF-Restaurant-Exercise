using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Restaurant.DTOs;
using WPF_Restaurant.Models;

namespace WPF_Restaurant.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        private readonly Order _order;

        public int OrderNumber => _order.Id;

        public IEnumerable<OrderItemViewModel> OrderItems => _order.Dishes.Select(x => new OrderItemViewModel(x));

        public OrderViewModel(Order order)
        {
            _order = order;
        }
    }
}
