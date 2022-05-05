using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Restaurant.Stores
{
	public class MessageStore : IMessageStore
	{
		public enum MessageType
		{
			Information,
			Error
		}

		private string _message;

		public string Message
		{
			get => _message;
			private set
			{
				_message = value;
				MessageChanged?.Invoke();
			}
		}

		private MessageType _type;

		public MessageType Type
		{
			get => _type;
			private set
			{
				_type = value;
				TypeChanged?.Invoke();
			}
		}

		public bool HasMessage => !string.IsNullOrEmpty(Message);

		public event Action MessageChanged;
		public event Action TypeChanged;

		public void SetMessage(string message, MessageType type)
		{
			Message = message;
			Type = type;
		}

		public void ClearMessage()
		{
			Message = string.Empty;
		}
	}
}
