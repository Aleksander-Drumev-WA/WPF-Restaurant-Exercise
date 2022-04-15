using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_Restaurant.Commands;
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
               IEnumerable<OrderItemViewModel> orderItems,
               Restaurant restaurant,
               MainChefViewModel mainChefViewModel,
               MessageStore messageStore,
               ILoggerFactory factory)
        {
            _orderNumber = orderNumber;
            RenderItems = new ObservableCollection<ChefLookingAtOrderItemViewModel>(
                orderItems.SelectMany(oi =>
                {
                return Enumerable.Repeat(oi, oi.Quantity)
                .Select((x, index) => new ChefLookingAtOrderItemViewModel(x.Name, x.Recipe, (x.RenderCount) > index, x.OrderNumber, x.Id));
                })
            );

            LoadOrdersCommand = new LoadOrdersCommand(mainChefViewModel.Orders, restaurant, messageStore);
            CompleteDishCommand = new CompleteDishCommand(restaurant, LoadOrdersCommand, messageStore, factory);
        }
    }
}
