﻿using Pegasssus.Common.Helpers;
using Pegasssus.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pegassus.Prism.ViewModels
{
    public class PegassusMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public PegassusMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }
        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        //TODO tener en cuenta el rol
        private void LoadMenus()
        {
            if (Settings.UserType=="Organizer")
            {
                var menus = new List<Menu>
                {
                    new Menu
                    {

                        Icon = "ic_event_note",
                        PageName = "EventsPage",
                        Title = "My Events"
                    },

                    new Menu
                    {
                        Icon = "ic_person",
                        PageName = "ProfilePage",
                        Title = "Modify Profile"
                    },

                    new Menu
                    {
                        Icon = "ic_exit_to_app",
                        PageName = "LoginPage",
                        Title = "Logout"
                    }
                };
                Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());
            }
            else
            {
                var menus = new List<Menu>
                {
                    new Menu
                    {

                        Icon = "ic_event_note",
                        PageName = "EventTabbedPage",
                        Title = "My Event"
                    },

                    new Menu
                    {
                        Icon = "ic_person",
                        PageName = "ProfilePage",
                        Title = "Modify Profile"
                    },

                    new Menu
                    {
                        Icon = "ic_exit_to_app",
                        PageName = "LoginPage",
                        Title = "Logout"
                    }
                };
                Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());
            }
            
        }
    }
}
