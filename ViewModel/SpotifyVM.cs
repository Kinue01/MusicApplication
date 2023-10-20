using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using MusicApplication.Messengers;
using MusicApplication.Model;
using MusicApplication.Utilities;
using SpotifyAPI;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Http;

namespace MusicApplication.ViewModel
{
    class SpotifyVM : ViewModelBase
    {
        private FullAlbum _album;
        private SimpleTrack _path;
        private FullTrack track;

        public FullAlbum Album
        {
            get { return _album; }
            set { _album = value; OnPropertyChanged(); }
        }

        public SimpleTrack SelectedItem
        {
            get { return _path; }
            set 
            { 
                _path = value; 
                OnPropertyChanged();
                //ConvertTracks();
                WeakReferenceMessenger.Default.Send(new UriMessenger(SelectedItem)); 
            }
        }

        public SpotifyVM()
        {
            PopulateCollection();
        }

        async void PopulateCollection()
        {
            try
            {
                var req = new ClientCredentialsRequest("578cbc9f3ee642e886c1f99cc43dc5a6", "4e29704a714f4fc0bfcf050b55cbdef6");
                var resp = await new OAuthClient().RequestToken(req);

                var spotify = new SpotifyClient(resp.AccessToken);

                Album = await spotify.Albums.Get("2M2Ae2SvZe3fmzUtlVOV5Z");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
            }
        }

        async void ConvertTracks()
        {
            var req = new ClientCredentialsRequest("578cbc9f3ee642e886c1f99cc43dc5a6", "4e29704a714f4fc0bfcf050b55cbdef6");
            var resp = await new OAuthClient().RequestToken(req);

            /*TokenAuthenticator authenticator = new(resp.AccessToken, resp.TokenType);

            APIConnector connector = new APIConnector(SpotifyUrls.APIV1, authenticator);

            TracksClient tracksClient = new(connector);

            TrackRequest trackRequest = new();
            trackRequest.Market = "FI";*/

            var spotify = new SpotifyClient(resp.AccessToken);

            track = await spotify.Tracks.Get("4XX5uZb9PvTKh8Nm2KSJfk");

            //track = await tracksClient.Get("4XX5uZb9PvTKh8Nm2KSJfk", trackRequest);
        }
    }
}