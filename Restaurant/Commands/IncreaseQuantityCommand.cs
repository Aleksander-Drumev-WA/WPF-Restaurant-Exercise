using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Restaurant.Extensions;
using WPF_Restaurant.Models;
using WPF_Restaurant.Stores;
using WPF_Restaurant.ViewModels;
using static WPF_Restaurant.Stores.MessageStore;

namespace WPF_Restaurant.Commands
{
    public class IncreaseQuantityCommand : BaseCommand
    {
        private readonly ObservableCollection<DishViewModel> _chosenDishes;
        private readonly MessageStore _messageStore;
        private readonly ILogger<IncreaseQuantityCommand> _logger;

        public IncreaseQuantityCommand(ObservableCollection<DishViewModel> chosenDishes, MessageStore messageStore, ILoggerFactory factory)
        {
            _chosenDishes = chosenDishes;
            _messageStore = messageStore;
            _logger = factory.CreateLogger<IncreaseQuantityCommand>();
        }

        public override void Execute(object? parameter)
        {
            try
            {
                _logger.LogInformation("Start increasing quantity...");
                _messageStore.SetMessage("Quantity increased!", MessageType.Information);
                var dishViewModel = new DishViewModel((Dish)parameter);

                var modelToChange = _chosenDishes.FirstOrDefault(cd => cd.Name == dishViewModel.Name);
                modelToChange.Quantity++;
                _logger.LogInformation("Quantity has been increased.");
            }
            catch (ArgumentNullException ane)
            {
                _messageStore.SetMessage(ane.Message, MessageType.Error);
                _logger.LogError(ane.GetExceptionData());
            }
            catch (Exception e)
            {
                _messageStore.SetMessage(e.Message, MessageType.Error);
                _logger.LogError(e.GetExceptionData());
            }

        }
    }
}
