namespace WPF_Restaurant.ViewModels.Customer;

public class DishViewModel : BaseViewModel
{
	private readonly Dish _dish;
	private int _quantity;

	public Dish Dish => _dish;

	public int Id => _dish.Id;

	public string Name => _dish.Name;

	public string ImagePath => _dish.ImagePath;

	public string Recipe => _dish.Recipe;

	public string Ingredients => _dish.Ingredients;

	public int Quantity
	{
		get => _quantity;
		set
		{
			_quantity = value;
			OnPropertyChanged(nameof(Quantity));
		}
	}

	public DishViewModel(Dish dish)
	{
		_dish = dish ?? throw new ArgumentNullException(nameof(dish));
		_quantity = 1;
	}
}