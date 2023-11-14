using CommunityToolkit.Mvvm.ComponentModel;
using MusicApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApplication.Services
{
    public interface ILoginService
    {
        string Name { get; }
        string Password { get; }
        void Login();
        void Registration();

    }
    internal class LoginService : ObservableObject, ILoginService
    {
        NeondbContext context = new();
        private string _name;
        private string _password;
        public string Name 
        {  
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }
        public string Password 
        { 
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        /*private readonly Func<Type, string> _nameFactory;
        private readonly Func<Type, string> _passwordFactory;

        public LoginService(Func<Type, string> nameFactory, Func<Type, string> passwordFactory)
        {
            _nameFactory = nameFactory;
            _passwordFactory = passwordFactory;
        }*/

        public void Login()
        {
            
        }

        public void Registration()
        {
            throw new NotImplementedException();
        }
    }
}
