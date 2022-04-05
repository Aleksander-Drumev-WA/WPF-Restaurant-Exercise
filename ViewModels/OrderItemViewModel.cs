using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Restaurant.DTOs;
using WPF_Restaurant.Models;

namespace WPF_Restaurant.ViewModels
{
    public class OrderItemViewModel : BaseViewModel
    {
        private readonly Dish _dish;
        private readonly int _quantity;
        private readonly int _orderNumber;

        public int Id => _dish.Id;

        public string Recipe => _dish.Recipe;

        public string Name => _dish.Name;

        public int Quantity => _quantity;

        public int OrderNumber => _orderNumber;

        public OrderItemViewModel(Dish dish, int quantity)
        {
            _dish = dish;
            _quantity = quantity;
        }

        public OrderItemViewModel(Dish dish, int quantity, int orderNumber) : this(dish, quantity)
        {
            _orderNumber = orderNumber;
        }
    }
}
