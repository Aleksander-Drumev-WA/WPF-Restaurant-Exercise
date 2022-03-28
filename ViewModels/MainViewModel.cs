using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Restaurant.Models;

namespace WPF_Restaurant.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public BaseViewModel CurrentViewModel { get; }

        public MainViewModel(Restaurant restaurant)
        {
            CurrentViewModel = MenuAndBasketViewModel.LoadViewModel(restaurant);
        }
    }
}
