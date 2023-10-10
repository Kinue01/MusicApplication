using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApplication.Messengers
{
    internal class MusicMessenger : ValueChangedMessage<TbMusic>
    {
        public MusicMessenger(TbMusic music) : base(music)
        {
        }
    }
}
