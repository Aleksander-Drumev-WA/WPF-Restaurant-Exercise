using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPF_Restaurant.Models;
using WPF_Restaurant.ViewModels;

namespace WPF_Restaurant.Commands
{
    public class ChooseDishCommand : BaseCommand
    {
        public ChooseDishCommand()
        {
            
        }

        public override void Execute(object? parameter)
        {
            //var order = new Order();
            
            //order.AddDish(new Dish(_dishViewModel.Name, _dishViewModel.ImagePath, _dishViewModel.Recipe, _dishViewModel.Quantity, _dishViewModel.Ingredients));
        }
    }
}
