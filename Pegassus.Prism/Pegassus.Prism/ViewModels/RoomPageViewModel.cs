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
        public RoomPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Room Details";
        }
    }
}
