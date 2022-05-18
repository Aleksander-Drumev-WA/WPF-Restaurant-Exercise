namespace WPF_Restaurant.Stores;

public class NavigationStore : BaseViewModel
{
	private BaseViewModel _currentViewModel;

	public BaseViewModel CurrentViewModel
	{
		get => _currentViewModel;
		set
		{
			_currentViewModel = value;
			OnCurrentViewModelChanged();
		}
	}

	private void OnCurrentViewModelChanged()
	{
		CurrentViewModelChanged?.Invoke();
	}

	public event Action CurrentViewModelChanged;
}