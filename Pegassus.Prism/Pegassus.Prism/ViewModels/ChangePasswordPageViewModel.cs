﻿using Newtonsoft.Json;
using Pegasssus.Common.Helpers;
using Pegasssus.Common.Models;
using Pegasssus.Common.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pegassus.Prism.ViewModels
{
    public class ChangePasswordPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _changePasswordCommand;

        public ChangePasswordPageViewModel(INavigationService navigationService, IApiService apiService):base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Change your password";
            IsEnabled = true;
            IsRunning = false;
        }

        public DelegateCommand ChangePasswordCommand => _changePasswordCommand ?? (_changePasswordCommand = new DelegateCommand(ChangePassword));

        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string PasswordConfirm { get; set; }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private async void ChangePassword()
        {
            var isValid = await ValidateData();
            if (!isValid)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;
           
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var request = AsingChangePasswordRequest();
            var url = App.Current.Resources["UrlAPI"].ToString();
            var response = await _apiService.ChangePasswordAsync(
                url,
                "/api",
                "/Account/ChangePassword",
                request,
                "bearer",
                token.Token);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            await App.Current.MainPage.DisplayAlert(
                "Ok",
                response.Message,
                "Accept");

            await _navigationService.GoBackAsync();
        }

        private ChangePasswordRequest AsingChangePasswordRequest()
        {
            if (Settings.UserType == "Organizer")
            {
                var organizer = JsonConvert.DeserializeObject<OrganizerResponse>(Settings.Organizer);
                return new ChangePasswordRequest
                {
                    Email = organizer.Email,
                    NewPassword = NewPassword,
                    OldPassword = CurrentPassword
                };
            }
            else
            {
                var invited = JsonConvert.DeserializeObject<InvitedResponse>(Settings.Invited);
                return new ChangePasswordRequest
                {
                    Email = invited.Email,
                    NewPassword = NewPassword,
                    OldPassword = CurrentPassword
                };
            }
        }

        private async Task<bool> ValidateData()
        {
            if (string.IsNullOrEmpty(CurrentPassword))
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter your current password.",
                    "Accept");
                return false;
            }

            if (string.IsNullOrEmpty(NewPassword) || NewPassword?.Length < 6)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a new password at least 6 characters length.",
                    "Accept");
                return false;
            }

            if (string.IsNullOrEmpty(PasswordConfirm))
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a password confirm.",
                    "Accept");
                return false;
            }

            if (!NewPassword.Equals(PasswordConfirm))
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "The new password and the confirmation does not match.",
                    "Accept");
                return false;
            }

            return true;
        }
    }
}
