using CommunityToolkit.Mvvm.Input;
using MauiApp1.apiCalls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.viewModel
{
    public partial class NewPage5ViewModel
    {
        userDetails Player;
        public NewPage5ViewModel(userDetails Player)
        {
            this.Player = Player;
        }
        [RelayCommand]
        public async Task navToShell()
        {
            foreach (string item in Player.tasks)
            {
                await Application.Current.MainPage.DisplayAlert("error", $"{item}", "OK");

            }

            var api = new updateWhole();
            bool success = await api.updateDocument(Player);

            if (success)
                await Application.Current.MainPage.DisplayAlert("Success", "User added", "OK");
            else
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to add user", "OK");

            Application.Current.MainPage = new AppShell();
        }
    }
}
