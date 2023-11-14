using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using MusicApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MusicApplication.Services
{
    interface IMusicPlayerService
    {
        void Play(int id);
        void PlayNext(int id);
        void PlayPrevious(int id);
        void Pause();
        void SetMusicData(int id);
    }

    internal class MusicPlayService : ObservableObject, IMusicPlayerService
    {
        MediaPlayer player = new();
        NeondbContext context = new();
        TbMusic temp;
        TbMusic data_temp;

        public TbMusic Temp 
        { 
            get => temp;
            set { temp = value; OnPropertyChanged(); }
        }

        public TbMusic Data_temp 
        { 
            get => data_temp;
            set { data_temp = value; OnPropertyChanged(); }
        }

        public void Pause()
        {
            player.Pause();
        }

        public async void Play(int id)
        {
            if (player.Source != null)
            {
                player.Stop();
            }
            Temp = await context.TbMusics.FindAsync(id);
            player.Open(new Uri(Temp.MusicPath));
            player.Play();
        }

        public async void PlayNext(int id)
        {
            if (player.Source != null)
            {
                player.Stop();
            }
            Temp = await context.TbMusics.FindAsync(id + 1);
            player.Open(new Uri(Temp.MusicPath));
            player.Play();
        }

        public async void PlayPrevious(int id)
        {
            if (player.Source != null)
            {
                player.Stop();
            }
            Temp = await context.TbMusics.FindAsync(id - 1);
            player.Open(new Uri(Temp.MusicPath));
            player.Play();
        }
        public async void SetMusicData(int id)
        {
            Data_temp = await context.TbMusics.FindAsync(id);
        }
    }
}
