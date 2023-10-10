using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MusicApplication.ViewModel;

namespace MusicApplication.View
{
    /// <summary>
    /// Логика взаимодействия для Search.xaml
    /// </summary>
    public partial class Search : UserControl
    {
        SearchVM vm;
        private ObservableCollection<TbMusic> musicList;
        public Search()
        {
            InitializeComponent();
            vm = new SearchVM();
            musicList = vm.Musics;
        }

        private void searchString_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                listBox_music.ItemsSource = musicList.Where(p => p.MusicName.Contains(searchString.Text));
            }
        }
    }
}
