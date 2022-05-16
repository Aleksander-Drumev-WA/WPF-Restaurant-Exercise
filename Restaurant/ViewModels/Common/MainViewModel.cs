namespace WPF_Restaurant.ViewModels.Common;

public class MainViewModel : BaseViewModel
{
	private readonly NavigationStore _navigationStore;

	public MainViewModel(NavigationStore navigationStore)
	{
		_navigationStore = navigationStore;
		_navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
	}

	private void OnCurrentViewModelChanged()
	{
		OnPropertyChanged(nameof(CurrentViewModel));
	}

	public BaseViewModel CurrentViewModel
	{
		get => _navigationStore.CurrentViewModel;
		set
		{
			_navigationStore.CurrentViewModel = value;
		}
	}
}