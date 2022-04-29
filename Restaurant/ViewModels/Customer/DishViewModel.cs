using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_Restaurant.Commands;
using WPF_Restaurant.Models;
using WPF_Restaurant.ViewModels.Common;

namespace WPF_Restaurant.ViewModels.Customer
{
    public class DishViewModel : BaseViewModel
    {
        private readonly Dish _dish;
        private int _quantity;

        public Dish Dish => _dish;

        public string Name => _dish.Name;

        public string ImagePath => _dish.ImagePath;

        public string Recipe => _dish.Recipe;

        public string Ingredients => _dish.Ingredients;

        public int Quantity 
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public DishViewModel(Dish dish)
        {
            _dish = dish;
            _quantity = 1;
        }
    }
}
