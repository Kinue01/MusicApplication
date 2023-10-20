using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MusicApplication.ViewModel;
using MusicApplication.Utilities;
using MusicApplication.Services;

namespace MusicApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainWindowVM>()
            });
            serviceCollection.AddSingleton<MainWindowVM>();
            serviceCollection.AddSingleton<HomeVM>();
            serviceCollection.AddSingleton<SearchVM>();
            serviceCollection.AddSingleton<SettingsVM>();
            serviceCollection.AddSingleton<SpotifyVM>();
            serviceCollection.AddSingleton<LoginVM>();
            serviceCollection.AddSingleton<INavigationService, NavigationService>();

            serviceCollection.AddSingleton<Func<Type, ViewModelBase>>(serviceProvider => viewModelType => (ViewModelBase)serviceProvider.GetRequiredService(viewModelType));

            _serviceProvider = serviceCollection.BuildServiceProvider();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }

    }
}
