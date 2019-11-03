using Newtonsoft.Json;
using Pegasssus.Common.Helpers;
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
    public class EventsPageViewModel : ViewModelBase
    {
        private OrganizerResponse _organizer;
        private InvitedResponse _invited;
        private ObservableCollection<EventItemViewModel> _events;
        private DelegateCommand _addEventCommand;
        private readonly INavigationService _navigationService;

        public EventsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadUser();            
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            LoadUser();
        }

        public DelegateCommand AddEventCommand => _addEventCommand ?? (_addEventCommand = new DelegateCommand(AddEvent));

        public ObservableCollection<EventItemViewModel> Events
        {
            get => _events;
            set => SetProperty(ref _events, value);
        }

        private void LoadUser()
        {
            if (Settings.UserType == "Organizer")
            {
                _organizer = JsonConvert.DeserializeObject<OrganizerResponse>(Settings.Organizer);
                Title = $"Events of: {_organizer.FirstName}";
                Events = new ObservableCollection<EventItemViewModel>(_organizer.Events.Select(p => new EventItemViewModel(_navigationService)
                {
                    InvitedsNumber = p.InvitedsNumber,
                    Id = p.Id,
                    Name = p.Name,
                    EventType = p.EventType,
                    Date = p.Date,
                }).ToList());
            }
            else
            {
                _invited = JsonConvert.DeserializeObject<InvitedResponse>(Settings.Invited);
                Title = $"Events of: {_organizer.FirstName}";
                Events = new ObservableCollection<EventItemViewModel>(_organizer.Events.Select(p => new EventItemViewModel(_navigationService)
                {
                    InvitedsNumber = p.InvitedsNumber,
                    Id = p.Id,
                    Name = p.Name,
                    EventType = p.EventType,
                    Date = p.Date,
                }).ToList());
            }
        }

        private async void AddEvent()
        {
            await _navigationService.NavigateAsync("EditEvent");
        }
    }
}
