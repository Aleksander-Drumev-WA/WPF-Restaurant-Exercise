using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Restaurant.Models;
using WPF_Restaurant.Stores;
using WPF_Restaurant.ViewModels;
using WPF_Restaurant.Views;
using static WPF_Restaurant.Stores.MessageStore;

namespace WPF_Restaurant.Commands
{
    public class ShowRecipeCommand : BaseCommand
    {
        private MainChefViewModel _mainChefViewModel;
        private readonly Restaurant _restaurant;
        private readonly MessageStore _messageStore;

        public ShowRecipeCommand(MainChefViewModel mainChefViewModel, Restaurant restaurant, MessageStore messageStore)
        {
            _mainChefViewModel = mainChefViewModel;
            _restaurant = restaurant;
            _messageStore = messageStore;
        }

        public override void Execute(object? parameter)
        {
            try
            {
                if (parameter is OrderItemViewModel)
                {
                    var incomingViewModel = (OrderItemViewModel)parameter;

                    var chosenDish = _mainChefViewModel.Orders
                        .SingleOrDefault(o => o.OrderNumber == incomingViewModel.OrderNumber)?.OrderItems
                        .FirstOrDefault(oi => oi.Id == incomingViewModel.Id);

                    if (chosenDish != null)
                    {
                        var viewModel = new ChefLookingAtRecipeViewModel(
                                        chosenDish,
                                        incomingViewModel.Id,
                                        _restaurant,
                                        _mainChefViewModel.Orders,
                                        _messageStore);

                        _mainChefViewModel.CurrentViewModel = viewModel;
                    }
                }
            }
            catch (ArgumentNullException ane)
            {
                _messageStore.SetMessage(ane.Message, MessageType.Error);
            }
            catch (Exception e)
            {
                _messageStore.SetMessage(e.Message, MessageType.Error);
            }
        }
    }
}
