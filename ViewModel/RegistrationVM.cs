using CommunityToolkit.Mvvm.Input;
using MusicApplication.Model;
using MusicApplication.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MusicApplication.ViewModel
{
    internal class RegistrationVM : ViewModelBase
    {
        private string _name;
        private string _password;
        NeondbContext context = new();
        int counter = 1;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public ICommand RegistrationCommand { get; }

        public RegistrationVM()
        {
            RegistrationCommand = new RelayCommand(OnRegistration);
        }

        void OnRegistration()
        {
            try
            {
                context.TbUsers.Add(new TbUser { UserId = counter++, UserName = Name, UserPassword = Password });
                context.SaveChanges();
                MessageBox.Show("Добавился");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
