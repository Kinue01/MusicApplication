using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using MusicApplication.Utilities;
using MusicApplication.Services;
using Npgsql;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MusicApplication.Messengers;
using System.Windows.Controls;

namespace MusicApplication.ViewModel
{
    internal partial class SearchVM : ViewModelBase
    {
        private readonly string connectionString = "Host=localhost;Port=5432;Database=musicplayer_db;Username=postgres;Password=Dtkjcbgtl2016";

        private TbMusic _music;
        private ObservableCollection<TbMusic> musics;

        public ObservableCollection<TbMusic> Musics
        {
            get => musics;
            set { SetProperty(ref musics, value); }
        }

        public TbMusic SelectedItem
        {
            get => _music;
            set 
            { _music = value; 
                OnPropertyChanged(); 
                WeakReferenceMessenger.Default.Send(new MusicMessenger(SelectedItem));
            }
        }

        public SearchVM()
        {
            _music = new TbMusic();

            NpgsqlConnection connection = new(connectionString);
            connection.Open();

            string sql = "select music_name, music_path from tb_music;";
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            List<string> temp = new();
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    temp.Add(reader.GetString(0));
                    temp.Add(reader.GetString(1));
                }
            }
            Musics = new ObservableCollection<TbMusic>()
            {
                new TbMusic(){MusicName = temp[0], MusicPath = temp[1]},
                new TbMusic(){MusicName = temp[2], MusicPath = temp[3]}
            };
        }
    }
}
