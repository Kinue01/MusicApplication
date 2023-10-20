using CommunityToolkit.Mvvm.Messaging.Messages;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApplication.Messengers
{
    internal class UriMessenger : ValueChangedMessage<SimpleTrack>
    {
        public UriMessenger(SimpleTrack value) : base(value) { }
    }
}
