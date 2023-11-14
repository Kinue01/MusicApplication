using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.Messaging;
using MusicApplication.Messengers;
using MusicApplication.ViewModel;
using SpotifyAPI.Web;

namespace MusicApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MediaPlayer player = new();
        DispatcherTimer timer = new();

        public MainWindow()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<MusicMessenger>(this, (r, m) =>
            {
                if (m.Value != null)
                {
                    player.Stop();
                    player.Close();
                    Slider_time.Value = 0;
                    player.Open(new Uri(m.Value.MusicPath));
                    if (player.Source != null)
                    {
                        player.MediaOpened += MediaOpened;
                    }
                }
            });
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (player.Source != null)
            {
                songTimerNow.Text = String.Format("{0}", player.Position.ToString(@"mm\:ss"));
                Slider_time.Value += 1;
            }
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void mainGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            if (!player.IsMuted)
            {
                timer.Stop();
                player.Pause();
            }

            if (player.IsMuted)
            {
                timer.Start();
                player.Play();
            }
        }

        private void Slider_time_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Slider_time.IsMouseOver)
            {
                player.Pause();
                player.Position = TimeSpan.FromSeconds(Slider_time.Value);
                player.Play();
            }
        }

        private void MediaOpened(object sender, EventArgs e)
        {
            timer.Start();
            Slider_time.Maximum = player.NaturalDuration.TimeSpan.TotalSeconds;
            songTimerAll.Text = String.Format("{0}", player.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
            player.Play();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
            Slider_time.Value = 0;
            //player.Open(new Uri());
            player.MediaOpened += MediaOpened;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            player.Stop();
            Slider_time.Value = 0;
            //player.Open(new Uri());
            player.MediaOpened += MediaOpened;
        }

        private void HideApp_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void StretchApp_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }
    }
}
