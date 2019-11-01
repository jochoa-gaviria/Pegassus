using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pegassus.Prism.ViewModels
{
    public class ChangePasswordPageViewModel : ViewModelBase
    {
        public ChangePasswordPageViewModel(INavigationService navigationService):base(navigationService)
        {
            Title = "Change your password";
        }
    }
}
