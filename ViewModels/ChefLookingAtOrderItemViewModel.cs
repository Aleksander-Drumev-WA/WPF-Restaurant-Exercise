using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_Restaurant.Commands;
using WPF_Restaurant.Models;

namespace WPF_Restaurant.ViewModels
{
    public class ChefLookingAtOrderItemViewModel : BaseViewModel
    {
        private bool _isCompleted;

        public ChefLookingAtOrderItemViewModel(string name, string recipe, bool isCompleted, int orderNumber, int dishId)
        {
            Name = name;
            Recipe = recipe;
            DishId = dishId;
            OrderNumber = orderNumber;
            _isCompleted = isCompleted;
        }

        public string Name { get; }

        public string Recipe { get; }

        public int DishId { get; }

        public int OrderNumber { get; }

        public bool IsCompleted 
        {
            get => _isCompleted;
            set
            {
                _isCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));
            }
        }
    }
}
