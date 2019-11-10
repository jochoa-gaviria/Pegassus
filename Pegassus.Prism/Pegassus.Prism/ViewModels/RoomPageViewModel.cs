using Newtonsoft.Json;
using Pegasssus.Common.Helpers;
using Pegasssus.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pegassus.Prism.ViewModels
{
    public class RoomPageViewModel : ViewModelBase
    {
        private RoomResponse _room;
        public RoomPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Room Details";
        }
        public RoomResponse Room
        {
            get => _room;
            set => SetProperty(ref _room, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Room = JsonConvert.DeserializeObject<RoomResponse>(Settings.Room);
        }


    }
}
