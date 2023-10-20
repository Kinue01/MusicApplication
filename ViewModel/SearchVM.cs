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
using Microsoft.EntityFrameworkCore;
using MusicApplication.Model;
using System.Net;

namespace MusicApplication.ViewModel
{
    internal partial class SearchVM : ViewModelBase
    {
        WebClient webClient = new();
        NeondbContext DbContext = new();

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
            ObservableCollection<TbMusic> temp = new ObservableCollection<TbMusic>();
            WeakReferenceMessenger.Default.Register<SelectionMessenger>(this, async (r, m) =>
            {
                temp.Clear();
                await DbContext.TbMusics.ForEachAsync((j) =>
                {
                    if (j.MusicName.Contains(m.Value))
                    {
                        temp.Add(j);
                    }
                });
            });
            return temp;
        }
    }
}
