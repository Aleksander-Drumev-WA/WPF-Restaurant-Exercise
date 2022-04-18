using Microsoft.Extensions.Logging;
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
using WPF_Restaurant.Extensions;
using static WPF_Restaurant.Stores.MessageStore;

namespace WPF_Restaurant.Commands
{
    public class DecreaseQuantityCommand : BaseCommand
    {
        private ObservableCollection<DishViewModel> _chosenDishes;
        private readonly MessageStore _messageStore;
        private readonly ILogger<DecreaseQuantityCommand> _logger;

        public DecreaseQuantityCommand(ObservableCollection<DishViewModel> chosenDishes, MessageStore messageStore, ILoggerFactory factory)
        {
            _chosenDishes = chosenDishes;
            _messageStore = messageStore;
            _logger = factory.CreateLogger<DecreaseQuantityCommand>();
        }

        public override void Execute(object? parameter)
        {
            try
            {
                _logger.LogInformation("Start decreasing quantity...");
                var dishViewModel = new DishViewModel((Dish)parameter);

                var modelToChange = _chosenDishes.FirstOrDefault(cd => cd.Name == dishViewModel.Name);
                modelToChange.Quantity--;
                _logger.LogInformation("Quantity has been decreased.");
                _messageStore.SetMessage("Quantity decresed!", MessageType.Information);
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
