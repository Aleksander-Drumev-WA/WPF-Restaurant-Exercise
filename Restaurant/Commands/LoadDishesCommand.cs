using System.Collections.ObjectModel;
using System.Threading.Tasks;

using static WPF_Restaurant.Stores.MessageStore;

namespace WPF_Restaurant.Commands;

public class LoadDishesCommand : AsyncBaseCommand
{
	private readonly ObservableCollection<DishViewModel> _dishesInMenu;
	private IDishProvider _dishProvider;
	private readonly IMessageStore _messageStore;
	private readonly ILogger _logger;

	public LoadDishesCommand(ObservableCollection<DishViewModel> dishesInMenu, IDishProvider dishProvider, IMessageStore messageStore, ILogger logger)
	{
		_dishesInMenu = dishesInMenu;
		_dishProvider = dishProvider;
		_messageStore = messageStore;
		_logger = logger;
	}

	public override async Task ExecuteAsync(object? parameter)
	{
		try
		{
			_logger.LogInformation("Start loading dishes...");
			var dishes = await _dishProvider.GetAllDishes();

			foreach (var dish in dishes.Select(d => new DishViewModel(d)))
			{
				_dishesInMenu.Add(dish);
			}
			_logger.LogInformation("Dishes have been loaded successfully.");

		}
		catch (Exception e)
		{
			_messageStore.SetMessage(e.Message, MessageType.Error);
			_logger.LogError(e.GetExceptionData());
		}
	}
}