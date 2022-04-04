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

        public string Name => _dish.Name;

        public int Quantity => _dish.Quantity;

        public OrderItemViewModel(Dish dish)
        {
            _dish = dish;
        }
    }
}
