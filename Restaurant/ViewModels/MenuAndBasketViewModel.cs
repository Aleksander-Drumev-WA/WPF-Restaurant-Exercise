using System.Collections.ObjectModel;

using static WPF_Restaurant.Stores.MessageStore;

namespace WPF_Restaurant.ViewModels;

public class MenuAndBasketViewModel : BaseViewModel
{
	private readonly ObservableCollection<DishViewModel> _dishesInMenu;

	private readonly ObservableCollection<DishViewModel> _chosenDishes;

	private ILogger _logger;
	private ILoggerFactory _loggerFactory;
	private IMessageStore _messageStore;
	private Restaurant _restaurant;

	public ObservableCollection<DishViewModel> DishesInMenu => _dishesInMenu;

	public ObservableCollection<DishViewModel> ChosenDishes => _chosenDishes;

	public MessageViewModel MessageViewModel { get; }

	public ICommand ChooseDishCommand { get; }

	public ICommand IncreaseQuantityCommand { get; }

	public ICommand DecreaseQuantityCommand { get; }

	public ICommand RemoveDishCommand { get; }

	public ICommand LoadDishesCommand { get; }

	public ICommand OrderCommand { get; }

	public ICommand NavigateCommand { get; }

	public MenuAndBasketViewModel(
		Restaurant restaurant,
		ICommand navigateCommand,
		IMessageStore messageStore,
		MessageViewModel messageViewModel,
		ILoggerFactory factory)
	{
		_restaurant = restaurant ?? throw new ArgumentNullException(nameof(restaurant));
		_loggerFactory = factory ?? throw new ArgumentNullException(nameof(factory));
		_dishesInMenu = new ObservableCollection<DishViewModel>();
		_chosenDishes = new ObservableCollection<DishViewModel>();
		ChooseDishCommand = new ChooseDishCommand(_chosenDishes, messageStore, factory.CreateLogger<ChooseDishCommand>());
		RemoveDishCommand = new RelayCommand(param => ExecuteRemoveDishCommand(param));
		LoadDishesCommand = new LoadDishesCommand(_dishesInMenu, restaurant.DishProvider, messageStore, factory.CreateLogger<LoadDishesCommand>());
		IncreaseQuantityCommand = new RelayCommand((param) => ExecuteChangeQuantityCommand(1, param));
		DecreaseQuantityCommand = new RelayCommand((param) => ExecuteChangeQuantityCommand(-1, param), (param) => CanChangeQuantity(param));
		LoadDishesCommand.Execute(null);
		OrderCommand = new CreateOrderCommand(_chosenDishes, restaurant.OrderCreator, messageStore, factory.CreateLogger<CreateOrderCommand>());
		NavigateCommand = navigateCommand ?? throw new ArgumentNullException(nameof(navigateCommand));
		MessageViewModel = messageViewModel ?? throw new ArgumentNullException(nameof(messageViewModel));
		_logger = factory.CreateLogger<MenuAndBasketViewModel>();
		_messageStore = messageStore ?? throw new ArgumentNullException(nameof(messageStore));
	}
	private void ExecuteChangeQuantityCommand(int amount, object param)
	{
		if (param is DishViewModel dishViewModel)
		{
			var newQuantity = dishViewModel.Quantity + amount;
			dishViewModel.Quantity = newQuantity < 1 ? 1 : newQuantity;
		}
	}

	private bool CanChangeQuantity(object param)
	{
		if (param is DishViewModel dishViewModel)
		{
			return dishViewModel.Quantity <= 1 ? false : true;
		}
		else
		{
			return false;
		}
	}

	private void ExecuteRemoveDishCommand(object param)
	{
		try
		{
			_logger.LogInformation("Start removing dish...");
			if (param is DishViewModel dishViewModel)
			{
				var dishToRemove = _chosenDishes.First(cd => cd.Id == dishViewModel.Id);
				_chosenDishes.Remove(dishToRemove);
				_messageStore.SetMessage("Dish has been removed.", MessageType.Information);
				_logger.LogInformation("Dish has been removed successfully.");
			}
			else
			{
				throw new ArgumentException("Wrong parameter passed to RemoveDishCommand");
			}
		}
		catch (Exception e)
		{
			_logger.LogError(e.GetExceptionData());
			_messageStore.SetMessage(e.Message, MessageType.Error);
		}
	}
}