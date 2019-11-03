using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pegassus.Prism.ViewModels
{
    public class AddInvitedsPageViewModel : ViewModelBase
    {
        public AddInvitedsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Add guests";
        }
    }
}
