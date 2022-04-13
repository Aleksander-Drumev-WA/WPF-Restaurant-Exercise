using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Restaurant.Models;
using WPF_Restaurant.Stores;
using WPF_Restaurant.ViewModels;
using static WPF_Restaurant.Stores.MessageStore;

namespace WPF_Restaurant.Commands
{
    public class ShowDishesInOrderCommand : BaseCommand
    {
        private MainChefViewModel _mainChefViewModel;
        private readonly Restaurant _restaurant;
        private readonly MessageStore _messageStore;

        public ShowDishesInOrderCommand(MainChefViewModel mainChefViewModel, Restaurant restaurant, MessageStore messageStore)
        {
            _mainChefViewModel = mainChefViewModel;
            _restaurant = restaurant;
            _messageStore = messageStore;
        }

        public override void Execute(object? parameter)
        {
            try
            {
                if (parameter is int incomingOrderNumber)
                {
                    var orderWithDishes = _mainChefViewModel.Orders.FirstOrDefault(o => o.OrderNumber == incomingOrderNumber);
                    if (orderWithDishes != null)
                    {
                        var viewModel = new ChefLookingAtOrderViewModel(
                            orderWithDishes.OrderNumber,
                            orderWithDishes.OrderItems,
                            _restaurant,
                            _mainChefViewModel,
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
