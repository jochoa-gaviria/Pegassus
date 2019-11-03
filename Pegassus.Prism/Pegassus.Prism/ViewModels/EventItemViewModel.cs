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
        private DelegateCommand _selectPetCommand;

        public EventItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectPetCommand => _selectPetCommand ?? (_selectPetCommand = new DelegateCommand(SelectEvent));

        private async void SelectEvent()
        {
            Settings.Event = JsonConvert.SerializeObject(this);
            await _navigationService.NavigateAsync("EventPage");
        }
    }
}
