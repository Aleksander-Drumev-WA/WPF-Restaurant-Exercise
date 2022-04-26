using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_Restaurant.Commands;
using WPF_Restaurant.DataAccess.Data;
using WPF_Restaurant.Models;
using WPF_Restaurant.Stores;

namespace WPF_Restaurant.ViewModels
{
    public class ChefLookingAtOrderViewModel : BaseViewModel
    {
        private int _orderNumber;

        public int OrderNumber => _orderNumber;

        public ObservableCollection<ChefLookingAtOrderItemViewModel> RenderItems { get; }

        public ICommand LoadOrdersCommand { get; }

        public ICommand CompleteDishCommand { get; }

		public ChefLookingAtOrderViewModel(int orderNumber,
               Order order,
               Restaurant restaurant,
               MainChefViewModel mainChefViewModel,
               MessageStore messageStore,
               ILoggerFactory factory,
               bool notReadyFilter)
        {
            _orderNumber = orderNumber;
            RenderItems = new ObservableCollection<ChefLookingAtOrderItemViewModel>(
                order.Dishes.Select(oi => new ChefLookingAtOrderItemViewModel(oi.Name, oi.Recipe, !(oi.IsCompleted), orderNumber, oi.Id))
            );

            LoadOrdersCommand = new LoadOrdersCommand(mainChefViewModel.Orders, restaurant, messageStore, factory);
            CompleteDishCommand = new CompleteDishCommand(restaurant, LoadOrdersCommand, messageStore, factory, mainChefViewModel);
        }
    }
}
