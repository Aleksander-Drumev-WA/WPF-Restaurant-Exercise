using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Restaurant.ViewModels;

namespace WPF_Restaurant.Views.SampleData
{
	internal class Data
	{
		public Data()
		{
			DishesInMenu = new List<DishViewModel> {
				new DishViewModel(new Models.Dish(1, "Name 1", "...", "recipe 1", new [] {"ingredients"})),
				new DishViewModel(new Models.Dish(2, "Name 2", "...", "recipe 2", new [] {"ingredients"})),
				new DishViewModel(new Models.Dish(3, "Name 3", "...", "recipe 3", new [] {"ingredients"})),
			};
		}

		public IEnumerable<DishViewModel> DishesInMenu { get; }
	}
}
