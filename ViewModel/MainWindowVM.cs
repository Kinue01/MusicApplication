﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore.Metadata;
using MusicApplication.Services;
using MusicApplication.Utilities;
using Npgsql;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Media;
using System.Net.Http.Headers;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.Messaging;
using MusicApplication.Messengers;
using MusicApplication.Model;
using Microsoft.EntityFrameworkCore;

namespace MusicApplication.ViewModel
{
    internal class MainWindowVM : ViewModelBase
    {
        NeondbContext context = new();

        private readonly TbMusic _music;
        private string _musicAuth;

        private INavigationService _navigation;
        public INavigationService Navigation
        { 
            get { return _navigation; }
            set => SetProperty(ref _navigation, value); 
        }

        public int Music_Id
        {
            get { return _music.MusicId; }
            set { _music.MusicId = value; OnPropertyChanged(); }
        }
        public string Music_Name
        {
            get { return _music.MusicName; }
            set { _music.MusicName = value; OnPropertyChanged(); }
        }
        public string Music_Path
        {
            get { return _music.MusicPath; }
            set { _music.MusicPath = value; OnPropertyChanged(); }
        }
        public int? Music_AuthorId
        {
            get { return _music.MusicAuthorId; }
            set { _music.MusicAuthorId = value; OnPropertyChanged(); }
        }
        public string Music_Author
        {
            get { return _musicAuth; }
            set { _musicAuth = value; OnPropertyChanged(); }
        }

        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigateSearchCommand { get; }
        public ICommand NavigateSettingsCommand { get; }
        public ICommand NavigateLoginCommand { get; }
        
        public MainWindowVM()
        {
            _music = new TbMusic();

            WeakReferenceMessenger.Default.Register<MusicMessenger>(this, async (r, m) =>
            {
                if (m.Value != null)
                {
                    Music_Name = m.Value.MusicName;
                    Music_Path = m.Value.MusicPath;
                    TbAuthor temp = await context.TbAuthors.FindAsync(m.Value.MusicAuthorId);
                    Music_Author = temp.AuthName;
                }
            });
        }

        public MainWindowVM(INavigationService navService) : this()
        {
            Navigation = navService;

            NavigateHomeCommand = new RelayCommand(Navigation.NavigateTo<HomeVM>) ;
            NavigateSettingsCommand = new RelayCommand(Navigation.NavigateTo<SettingsVM>);
            NavigateSearchCommand = new RelayCommand(Navigation.NavigateTo<SearchVM>);
            NavigateLoginCommand = new RelayCommand(Navigation.NavigateTo<LoginVM>);
        }
    }
}
