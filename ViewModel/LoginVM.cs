using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using MusicApplication.Model;
using MusicApplication.Services;
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
    internal class LoginVM : ViewModelBase
    {
        private string _name;
        private string _password;
        NeondbContext context = new();
        private INavigationService _navigation;

        public INavigationService Navigation
        {
            get { return _navigation; }
            set => SetProperty(ref _navigation, value);
        }

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

        public ICommand LoginCommand { get; }
        public ICommand RegistrationCommand { get; }

        public LoginVM(INavigationService navService)
        {
            _navigation = navService;

            LoginCommand = new RelayCommand(OnLogin);
            RegistrationCommand = new RelayCommand(Navigation.NavigateTo<RegistrationVM>);
        }

        async void OnLogin()
        {
            try
            {
                TbUser temp = await context.TbUsers.FirstOrDefaultAsync(u => u.UserName == Name && u.UserPassword == Password);
                if (temp != null)
                {
                    MessageBox.Show("Получилось");
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Не удалось");
            }
        }
    }
}
