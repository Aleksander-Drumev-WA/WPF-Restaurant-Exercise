using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Restaurant.ViewModels
{
    public class ChefLookingAtOrderViewModel : BaseViewModel
    {
        private int _orderNumber;
        private IEnumerable<OrderItemViewModel> _orderItems;

        public int OrderNumber => _orderNumber;

        public IEnumerable<OrderItemViewModel> OrderItems => _orderItems;

        public ChefLookingAtOrderViewModel(int orderNumber, IEnumerable<OrderItemViewModel> orderItems)
        {
            _orderNumber = orderNumber;
            _orderItems = orderItems;
        }
    }
}
