using static WPF_Restaurant.Stores.MessageStore;

namespace WPF_Restaurant.ViewModels.Common;

public class MessageViewModel : BaseViewModel
{
	private readonly MessageStore _messageStore;

	public string Message => _messageStore.Message;

	public bool IsInformationMessage => _messageStore.Type == MessageType.Information;

	public bool IsErrorMessage => _messageStore.Type == MessageType.Error;

	public bool HasMessage => _messageStore.HasMessage;

	public ICommand ClearMessageCommand { get; }

	public MessageViewModel(MessageStore messageStore)
	{
		_messageStore = messageStore;

		_messageStore.MessageChanged += MessageStore_MessageChanged;
		_messageStore.TypeChanged += MessageStore_TypeChanged;
		ClearMessageCommand = new RelayCommand(param => _messageStore.ClearMessage());
	}

	private void MessageStore_TypeChanged()
	{
		OnPropertyChanged(nameof(IsInformationMessage));
		OnPropertyChanged(nameof(IsErrorMessage));
	}

	private void MessageStore_MessageChanged()
	{
		OnPropertyChanged(nameof(Message));
		OnPropertyChanged(nameof(HasMessage));
	}
}