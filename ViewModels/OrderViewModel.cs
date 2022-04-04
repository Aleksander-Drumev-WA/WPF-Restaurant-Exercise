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

        public IEnumerable<OrderItemViewModel> OrderItems => _order.Dishes.DistinctBy(d => d.Id)
                                                             .Select(d => new OrderItemViewModel(d.Name, _order.Dishes.Count(x => x.Id == d.Id)));

        public OrderViewModel(Order order)
        {
            _order = order;
        }
    }
}
