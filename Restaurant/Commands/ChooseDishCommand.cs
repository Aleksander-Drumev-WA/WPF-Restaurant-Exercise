using System.Collections.ObjectModel;

using static WPF_Restaurant.Stores.MessageStore;

namespace WPF_Restaurant.Commands;

public class ChooseDishCommand : BaseCommand
{
	private readonly ObservableCollection<DishViewModel> _chosenDishes;
	private readonly IMessageStore _messageStore;
	private readonly ILogger _logger;

	public ChooseDishCommand(ObservableCollection<DishViewModel> chosenDishes, IMessageStore messageStore, ILogger logger)
	{
		_chosenDishes = chosenDishes;
		_messageStore = messageStore;

		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	public override void Execute(object? parameter)
	{
		try
		{
			_logger.LogInformation("Begin choosing a dish...");
			if (parameter is Dish dish)
			{
				_chosenDishes.Add(new DishViewModel(dish));
				_messageStore.SetMessage("Dish has been chosen.", MessageType.Information);
				_logger.LogInformation("Choosing dish completed successfully.");
			}
			else
			{
				throw new ArgumentException("Wrong parameter passed to ChooseDishCommand");
			}
		}
		catch (Exception e)
		{
			_messageStore.SetMessage(e.Message, MessageType.Error);
			_logger.LogError(e.GetExceptionData());

		}
	}
}