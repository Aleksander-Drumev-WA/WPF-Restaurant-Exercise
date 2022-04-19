﻿using Microsoft.Extensions.Logging;
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
    public class RemoveDishCommand : BaseCommand
    {
        private ObservableCollection<DishViewModel> _chosenDishes;
        private readonly MessageStore _messageStore;
        private readonly ILogger<RemoveDishCommand> _logger;

        public RemoveDishCommand(ObservableCollection<DishViewModel> chosenDishes, MessageStore messageStore, ILoggerFactory factory)
        {
            _chosenDishes = chosenDishes;
            _messageStore = messageStore;
            _logger = factory.CreateLogger<RemoveDishCommand>();
        }

        public override void Execute(object? parameter)
        {
            try
            {
                _logger.LogInformation("Start removing dish...");
                _messageStore.SetMessage("Dish has been removed.", MessageType.Information);
                var dishViewModel = new DishViewModel((Dish)parameter);

                var dishToRemove = _chosenDishes.FirstOrDefault(cd => cd.Name == dishViewModel.Name);
                _chosenDishes.Remove(dishToRemove);
                _logger.LogInformation("Dish has been removed successfully.");
            }
            catch (ArgumentNullException ane)
            {
                _logger.LogError(ane.GetExceptionData());
                _messageStore.SetMessage(ane.Message, MessageType.Error);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetExceptionData());
                _messageStore.SetMessage(e.Message, MessageType.Error);
            }
        }
    }
}