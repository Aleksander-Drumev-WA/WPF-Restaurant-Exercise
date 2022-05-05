using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using WPF_Restaurant.Commands;
using WPF_Restaurant.DataAccess.Data;
using WPF_Restaurant.Models;
using WPF_Restaurant.Stores;
using WPF_Restaurant.ViewModels.Common;

namespace WPF_Restaurant.ViewModels.Chef
{
    public class ChefLookingAtOrderViewModel : BaseViewModel
    {
        private int _orderNumber;

        public int OrderNumber => _orderNumber;

        public ObservableCollection<ChefLookingAtOrderItemViewModel> RenderItems { get; }

        public ICommand LoadOrdersCommand { get; }

        public ICommand CompleteDishCommand { get; }

		public ChefLookingAtOrderViewModel(
               Order order,
               Restaurant restaurant,
               MainChefViewModel mainChefViewModel,
               MessageStore messageStore,
               ILoggerFactory factory,
               bool notReadyFilter)
        {
            _orderNumber = order.Id;
            RenderItems = new ObservableCollection<ChefLookingAtOrderItemViewModel>(
                order.Dishes.Select(oi => new ChefLookingAtOrderItemViewModel(oi.Name, oi.Recipe, !(oi.IsCompleted), order.Id, oi.Id))
            );

            LoadOrdersCommand = new LoadOrdersCommand(mainChefViewModel?.Orders, restaurant, messageStore, factory);
            CompleteDishCommand = new CompleteDishCommand(restaurant, LoadOrdersCommand, messageStore, factory, mainChefViewModel);
        }
    }
}
