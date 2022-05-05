namespace WPF_Restaurant.Stores
{
	public interface IMessageStore
	{
		void ClearMessage();
		void SetMessage(string message, MessageStore.MessageType type);
	}
}