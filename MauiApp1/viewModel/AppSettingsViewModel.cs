using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.viewModel
{
    public partial class AppSettingsViewModel
    {
        [RelayCommand]
        public void logOut()
        {
            Preferences.Remove("email");
            Preferences.Remove("isLoggedIn");
        }
    }
}
