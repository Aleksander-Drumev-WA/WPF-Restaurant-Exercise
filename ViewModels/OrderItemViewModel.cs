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
        private readonly string _name;
        private readonly int _quantity;

        public string Name => _name;

        public int Quantity => _quantity;

        public OrderItemViewModel(string name, int quantity)
        {
            _name = name;
            _quantity = quantity;
        }
    }
}
