using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_Restaurant.Commands;
using WPF_Restaurant.Stores;

namespace WPF_Restaurant.ViewModels
{
    public class MainChefViewModel : BaseViewModel
    {
        public ICommand NavigateCommand { get; }

        public MainChefViewModel(NavigationStore navigationStore, Func<MenuAndBasketViewModel> createMenuAndBasketViewModel)
        {
            NavigateCommand = new NavigateCommand(navigationStore, createMenuAndBasketViewModel);
        }
    }
}
