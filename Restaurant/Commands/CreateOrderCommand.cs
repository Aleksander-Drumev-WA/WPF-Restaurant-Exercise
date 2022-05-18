using Models.Models;
using System.Threading.Tasks;

using static WPF_Restaurant.Stores.MessageStore;

namespace WPF_Restaurant.Commands;

public class CreateOrderCommand : AsyncBaseCommand
{
	private IEnumerable<DishViewModel> _chosenDishes;
	private readonly IOrderCreator _orderCreator;
	private readonly IMessageStore _messageStore;
	private readonly ILogger _logger;

	public CreateOrderCommand(IEnumerable<DishViewModel> chosenDishes, IOrderCreator orderCreator, IMessageStore messageStore, ILogger logger)
	{
		_chosenDishes = chosenDishes;
		_orderCreator = orderCreator;
		_messageStore = messageStore;
		_logger = logger;
	}

	public override async Task ExecuteAsync(object? parameter)
	{
		try
		{
			_logger.LogInformation("Creating order...");
			var dishes = _chosenDishes.Select(x => new CartItem(x.Dish, x.Quantity));

			await _orderCreator.CreateOrder(dishes);

			_messageStore.SetMessage("Successfully created an order.", MessageType.Information);
			_logger.LogInformation("Successfully created an order.");
		}
		catch (Exception e)
		{
			_messageStore.SetMessage(e.Message, MessageType.Error);
			_logger.LogError(e.GetExceptionData());
		}
	}
}