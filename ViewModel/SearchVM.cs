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
        private readonly string connectionString = "Host=ep-polished-glade-22606167.eu-central-1.aws.neon.tech;Port=5432;Database=neondb;Username=tverdohlebovartem;Password=1aYkNAxZhLO8";

        private TbMusic _music;
        private ObservableCollection<TbMusic> _musicList;

        public TbMusic SelectedItem
        {
            get => _music;
            set 
            {   _music = value;
                OnPropertyChanged(); 
                WeakReferenceMessenger.Default.Send(new MusicMessenger(SelectedItem));
            }
        }

        public ObservableCollection<TbMusic> MusicList 
        { 
            get => _musicList; 
            set => SetProperty(ref _musicList, value); 
        }

        public SearchVM()
        {
            _music = new TbMusic();
            MusicList = OnSelectMusic();
        }

        private ObservableCollection<TbMusic> OnSelectMusic()
        {
            ObservableCollection<TbMusic> temp = new();
            WeakReferenceMessenger.Default.Register<SelectionMessenger>(this, async (r, m) =>
            {
                temp.Clear();
                NpgsqlConnection connection = new(connectionString);
                await Task.Run(() => connection.Open());

                string sql = $"select music_name, music_path, music_author_id from tb_music where music_name like '%{m.Value}%';";
                NpgsqlCommand command = new(sql, connection);
                int counter = 0;
                NpgsqlDataReader reader = command.ExecuteReader();
                    while (await Task.Run( () => reader.Read()))
                    {
                        temp.Add(new TbMusic { MusicName = reader.GetString(0), MusicPath = reader.GetString(1), MusicAuthorId = reader.GetInt32(2) });
                        counter++;
                    }
            });
            return temp;
        }
    }
}
