using DataAccess.Abstractions;
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

        // Why is it "public"? I can find "Binding" for this command.
        // It looks like "We wrap "action" in command just for transfer an action".
        public ICommand LoadOrdersCommand { get; }

        public ICommand CompleteDishCommand { get; }

		public ChefLookingAtOrderViewModel(
               Order order,
               IOrderProvider orderProvider,
               MainChefViewModel mainChefViewModel,
               IMessageStore messageStore,
               ILoggerFactory factory)
        {
            _orderNumber = order.Id;
            RenderItems = new ObservableCollection<ChefLookingAtOrderItemViewModel>(
                order.Dishes.Select(oi => new ChefLookingAtOrderItemViewModel(oi.Name, oi.Recipe, !(oi.IsCompleted), order.Id, oi.Id))
            );

            LoadOrdersCommand = new LoadOrdersCommand(mainChefViewModel?.Orders, orderProvider, messageStore, factory.CreateLogger<LoadOrdersCommand>());
            // I'm trying to understand: why we create command here, we can pass it as a parameter.
            // This command "lives" only when MainChefViewModel "lives" and this ViewModel "lives" only when MainChefViewModel "lives".
            // so we can create this command in MainChefViewModel and transfer the instance of command to "child" view models. 
            // "Child" view models live only in MainChefViewModel. It's simplify dependencies of ChefLookingAtOrderViewModel and ChefLookingAtRecipeViewModel.
            // The constructor of ChefLookingAtOrderViewModel will be like:
            // public ChefLookingAtOrderViewModel(Order order, ICommand completeDishCommand) { ... }
            // The constructor of ChefLookingAtRecipeViewModel will be like:
            // public ChefLookingAtRecipeViewModel(OrderItemViewModel chosenDish, ICommand completeDishCommand) { ... }
            // Do you agree?
            // less dependencies - less bugs
            CompleteDishCommand = new CompleteDishCommand(orderProvider, LoadOrdersCommand, messageStore, factory.CreateLogger<CompleteDishCommand>(), mainChefViewModel);
            // If you move creating of CompleteDishCommand to MainChefViewModel, you will see - CompleteDishCommand is RelayCommand! I like RelayCommand :-)
        }
    }
}
