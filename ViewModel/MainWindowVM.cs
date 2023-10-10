using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore.Metadata;
using MusicApplication.Services;
using MusicApplication.Utilities;
using Npgsql;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Media;
using System.Net.Http.Headers;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.Messaging;
using MusicApplication.Messengers;

namespace MusicApplication.ViewModel
{
    internal class MainWindowVM : ViewModelBase
    {
        private readonly string connectionString = "Host=localhost;Username=postgres;Password=Dtkjcbgtl2016;Database=musicplayer_db";

        private readonly TbMusic _music;
        private string _musicAuth;

        private INavigationService _navigation;
        public INavigationService Navigation
        { 
            get { return _navigation; }
            set => SetProperty(ref _navigation, value); 
        }

        public int Music_Id
        {
            get { return _music.MusicId; }
            set { _music.MusicId = value; OnPropertyChanged(); }
        }
        public string Music_Name
        {
            get { return _music.MusicName; }
            set { _music.MusicName = value; OnPropertyChanged(); }
        }
        public string Music_Path
        {
            get { return _music.MusicPath; }
            set { _music.MusicPath = value; OnPropertyChanged(); }
        }
        public int? Music_AuthorId
        {
            get { return _music.MusicAuthorId; }
            set { _music.MusicAuthorId = value; OnPropertyChanged(); }
        }
        public string Music_Author
        {
            get { return _musicAuth; }
            set { _musicAuth = value; OnPropertyChanged(); }
        }

        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigateSearchCommand { get; set; }
        public ICommand NavigateSettingsCommand { get; set; }

        public MainWindowVM()
        {
            _music = new TbMusic();

            NpgsqlConnection connection = new(connectionString);
            connection.Open();

            string sql1 = "select * from tb_music where music_id = 1;";
            NpgsqlCommand command1 = new(sql1, connection);

            using (NpgsqlDataReader reader = command1.ExecuteReader())
            {
                while (reader.Read())
                {
                    Music_Id = reader.GetInt32(0);
                    Music_Name = reader.GetString(1);
                    Music_Path = reader.GetString(2);
                    Music_AuthorId = reader.GetInt32(3);
                }
            }

            string sql2 = $"select auth_name from tb_author where auth_id = {Music_AuthorId};";
            NpgsqlCommand command2 = new NpgsqlCommand(sql2, connection);
            Music_Author = command2.ExecuteScalar().ToString();

            WeakReferenceMessenger.Default.Register<MusicMessenger>(this, (r, m) =>
            {
                Music_Name = m.Value.MusicName;
                Music_Path = m.Value.MusicPath;
            });
        }

        public MainWindowVM(INavigationService navService) : this()
        {
            Navigation = navService;

            NavigateHomeCommand = new RelayCommand(Navigation.NavigateTo<HomeVM>) ;
            NavigateSettingsCommand = new RelayCommand(Navigation.NavigateTo<SettingsVM>);
            NavigateSearchCommand = new RelayCommand(Navigation.NavigateTo<SearchVM>) ;
        }
    }
}
