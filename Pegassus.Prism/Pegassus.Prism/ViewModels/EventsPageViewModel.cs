using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pegassus.Prism.ViewModels
{
    public class EventsPageViewModel : ViewModelBase
    {
        public EventsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Events";
        }
    }
}
