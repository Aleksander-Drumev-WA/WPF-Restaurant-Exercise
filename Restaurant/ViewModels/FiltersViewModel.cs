using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Restaurant.ViewModels
{
	public class FiltersViewModel : BaseViewModel
	{

		private bool _notReadyCheck;
		private string _nameFilter;

		public bool NotReadyFilterChecked 
		{ 
			get => _notReadyCheck;
			set
			{
				_notReadyCheck = value;
				OnPropertyChanged(nameof(NotReadyFilterChecked));
			}
		}

		public string NameFilter 
		{
			get => _nameFilter;
			set
			{
				_nameFilter = value;
				OnPropertyChanged(nameof(NameFilter));
			}
		}
	}
}
