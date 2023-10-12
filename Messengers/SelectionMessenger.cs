using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApplication.Messengers
{
    internal class SelectionMessenger : ValueChangedMessage<string>
    {
        public SelectionMessenger(string value) : base(value)
        {
        }
    }
}
