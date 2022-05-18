namespace WPF_Restaurant.ViewModels.Chef;

public class ChefLookingAtRecipeViewModel : BaseViewModel
{
	private readonly OrderItemViewModel _chosenDish;

	public int DishId => _chosenDish.Id;

	public int OrderNumber => _chosenDish.OrderNumber;

	public string DishName => _chosenDish.Name;

	public string DishRecipe => _chosenDish.Recipe;

	public ICommand CompleteDishCommand { get; }


	public ChefLookingAtRecipeViewModel(OrderItemViewModel chosenDish, ICommand completeDishCommand)
	{
		_chosenDish = chosenDish;
		CompleteDishCommand = completeDishCommand;
	}
}