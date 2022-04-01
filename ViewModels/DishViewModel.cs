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
    public class DishViewModel : BaseViewModel
    {
        private readonly Dish _dish;

        public Dish Dish => _dish;

        public string Name => _dish.Name;

        public string ImagePath => _dish.ImagePath;

        public string Recipe => _dish.Recipe;

        public string Ingredients => _dish.Ingredients;

        public int Quantity 
        {
            get => _dish.Quantity;
            set
            {
                _dish.Quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public DishViewModel(Dish dish)
        {
            _dish = dish;
        }
    }
}
