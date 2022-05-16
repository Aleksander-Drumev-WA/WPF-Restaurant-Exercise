using System.Collections.ObjectModel;

namespace WPF_Restaurant.ViewModels.Chef;

public class ChefLookingAtOrderViewModel : BaseViewModel
{
	private int _orderNumber;

	public int OrderNumber => _orderNumber;

	public ObservableCollection<ChefLookingAtOrderItemViewModel> RenderItems { get; }

	public ICommand CompleteDishCommand { get; }

	public ChefLookingAtOrderViewModel(Order order, ICommand completeDishCommand)
	{
		_orderNumber = order.Id;
		RenderItems = new ObservableCollection<ChefLookingAtOrderItemViewModel>(
			order.Dishes.Select(oi => new ChefLookingAtOrderItemViewModel(oi.Name, oi.Recipe, !(oi.IsCompleted), order.Id, oi.Id))
		);

		CompleteDishCommand = completeDishCommand;
	}
}