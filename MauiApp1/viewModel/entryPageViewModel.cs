using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.viewModel
{
    public partial class entryPageViewModel
    {
        [RelayCommand]
        public void navToBody()
        {
            bool exist = Microsoft.Maui.Storage.Preferences.Get("isLoggedIn", false);
            if (exist)
                Application.Current.MainPage = new AppShell();
            else
                Application.Current.MainPage.Navigation.PushAsync(new MauiApp1.loginPage());
        }
    }
}
