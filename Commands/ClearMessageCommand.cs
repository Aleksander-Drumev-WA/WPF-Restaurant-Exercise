using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Restaurant.Stores;

namespace WPF_Restaurant.Commands
{
    public class ClearMessageCommand : BaseCommand
    {
        private readonly MessageStore _messageStore;

        public ClearMessageCommand(MessageStore messageStore)
        {
            _messageStore = messageStore;
        }

        public override void Execute(object? parameter)
        {
            _messageStore.ClearMessage();
        }
    }
}
