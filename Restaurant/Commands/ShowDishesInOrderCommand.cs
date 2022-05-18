using static WPF_Restaurant.Stores.MessageStore;

namespace WPF_Restaurant.Commands;

public class ShowDishesInOrderCommand : BaseCommand
{
	private MainChefViewModel _mainChefViewModel;
	private readonly IOrderProvider _orderProvider;
	private readonly IMessageStore _messageStore;
	private readonly ILoggerFactory _factory;
	private readonly ILogger<ShowDishesInOrderCommand> _logger;

	public ShowDishesInOrderCommand(MainChefViewModel mainChefViewModel, IOrderProvider orderProvider, IMessageStore messageStore, ILoggerFactory factory)
	{
		_mainChefViewModel = mainChefViewModel;
		_orderProvider = orderProvider;
		_messageStore = messageStore;
		_factory = factory;
		_logger = factory.CreateLogger<ShowDishesInOrderCommand>();
	}

	public override void Execute(object? parameter)
	{
		try
		{
			if (parameter is int incomingOrderNumber)
			{
				_logger.LogInformation("Start showing dishes in an order...");
				var orderWithDishes = _mainChefViewModel.Orders.FirstOrDefault(o => o.OrderNumber == incomingOrderNumber);
				if (orderWithDishes != null)
				{
					var viewModel = new ChefLookingAtOrderViewModel(
						orderWithDishes.Order,
						_mainChefViewModel.CompleteDishCommand);

					_mainChefViewModel.CurrentViewModel = viewModel;
					_logger.LogInformation("Dishes in order have been shown successfully.");
				}
				else if (orderWithDishes == null)
				{
					_logger.LogWarning("Cannot show dishes in a non-existent order.");
				}
			}
			else
			{
				_logger.LogWarning("Invalid parameter passed in ShowDishesInOrderCommand");
			}
		}
		catch (Exception e)
		{
			_messageStore.SetMessage(e.Message, MessageType.Error);
			_logger.LogError(e.GetExceptionData());
		}
	}
}