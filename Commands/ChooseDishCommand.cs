using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPF_Restaurant.Models;
using WPF_Restaurant.Stores;
using WPF_Restaurant.ViewModels;
using static WPF_Restaurant.Stores.MessageStore;

namespace WPF_Restaurant.Commands
{
    public class ChooseDishCommand : BaseCommand
    {
        private readonly ObservableCollection<DishViewModel> _chosenDishes;
        private readonly MessageStore _messageStore;
        private DishViewModel _dishViewModel;
        private Dish? _dish;

        public DishViewModel ChosenDish => _dishViewModel;

        public ChooseDishCommand(ObservableCollection<DishViewModel> chosenDishes, MessageStore messageStore)
        {
            _chosenDishes = chosenDishes;
            _messageStore = messageStore;
        }

        public override void Execute(object? parameter)
        {
            try
            {
                _dish = (Dish?)parameter;
                _dishViewModel = new DishViewModel(_dish);
                _chosenDishes.Add(_dishViewModel);
            }
            // Logging later
            catch (ArgumentNullException ex)
            {
                _messageStore.SetMessage(ex.Message, MessageType.Error);
            }
            catch (Exception e)
            {
                _messageStore.SetMessage(e.Message, MessageType.Error);
            }
        }
    }
}
