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
        public const string auth_token = "auth_token";

        [RelayCommand]
        public async Task logOut()
        {
            SecureStorage.Remove(auth_token);
            var page = ((App)Application.Current).Services.GetRequiredService<loginPage>();

            Application.Current.MainPage = new NavigationPage(page);

        }
    }
}
