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
    public class EventItemViewModel : EventResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectEventCommand;

        public EventItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectEventCommand => _selectEventCommand ?? (_selectEventCommand = new DelegateCommand(SelectEvent));

        private async void SelectEvent()
        {
            Settings.Event = JsonConvert.SerializeObject(this);
            await _navigationService.NavigateAsync("EventPage");
        }
    }
}
