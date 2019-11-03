using Newtonsoft.Json;
using Pegasssus.Common.Helpers;
using Pegasssus.Common.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pegassus.Prism.ViewModels
{
    public class RoomItemViewModel : RoomResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectRoomCommand;

        public RoomItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectRoomCommand => _selectRoomCommand ?? (_selectRoomCommand = new DelegateCommand(SelectRoom));

        private async void SelectRoom()
        {
            Settings.Room = JsonConvert.SerializeObject(this);
            await _navigationService.NavigateAsync("RoomPage");
        }
    }
}
