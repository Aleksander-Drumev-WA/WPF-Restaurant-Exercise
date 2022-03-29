using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Restaurant.Models;
using WPF_Restaurant.Stores;

namespace WPF_Restaurant.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;

        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public BaseViewModel CurrentViewModel
        {
            get => _navigationStore.CurrentViewModel;
            set
            {
                _navigationStore.CurrentViewModel = value;
            }
        }
    }
}
