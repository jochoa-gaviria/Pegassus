﻿using Prism;
using Prism.Ioc;
using Pegassus.Prism.ViewModels;
using Pegassus.Prism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pegasssus.Common.Services;
using Newtonsoft.Json;
using Pegasssus.Common.Helpers;
using Pegasssus.Common.Models;
using System;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Pegassus.Prism
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTU5NjU0QDMxMzcyZTMzMmUzMGorR2RMTlpkbDNYN09zTW5BaG9FTEJCZDJXa28rQXVyNzJRSGFUSEhMZWc9");

            InitializeComponent();

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            if (Settings.IsRemembered && token?.Expiration > DateTime.Now)
            {
                if (Settings.UserType=="Organizer")
                {
                    //await NavigationService.NavigateAsync("/PegasssusMasterDetailPage/NavigationPage/EventsPage");
                    await NavigationService.NavigateAsync("/PegassusMasterDetailPage/NavigationPage/EventsPage");
                }
                else
                {
                    await NavigationService.NavigateAsync("/PegassusMasterDetailPage/NavigationPage/EventTabbedPage");
                }
            }
            else
            {
                await NavigationService.NavigateAsync("/NavigationPage/LoginPage");
                //await NavigationService.NavigateAsync("PegassusMasterDetailPage");
            }

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<RememberPasswordPage, RememberPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<PegassusMasterDetailPage, PegassusMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<EventsPage, EventsPageViewModel>();
            containerRegistry.RegisterForNavigation<EventPage, EventPageViewModel>();
            containerRegistry.RegisterForNavigation<AgendaPage, AgendaPageViewModel>();
            containerRegistry.RegisterForNavigation<MapPage, MapPageViewModel>();
            containerRegistry.RegisterForNavigation<ProfilePage, ProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<ChangePasswordPage, ChangePasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<EditEvent, EditEventViewModel>();
            containerRegistry.RegisterForNavigation<RoomsPage, RoomsPageViewModel>();
            containerRegistry.RegisterForNavigation<AddInvitedsPage, AddInvitedsPageViewModel>();
            containerRegistry.RegisterForNavigation<RoomPage, RoomPageViewModel>();
            containerRegistry.RegisterForNavigation<EventTabbedPage, EventTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<InvitedsPage, InvitedsPageViewModel>();
        }
    }
}
