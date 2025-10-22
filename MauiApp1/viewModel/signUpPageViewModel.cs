using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.apiCalls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.viewModel
{
    public partial class signUpPageViewModel: ObservableObject
    {
        public userDetails player;
        [ObservableProperty]
         string usernameEntry;
        public signUpPageViewModel(userDetails player)
        {
            this.player = player;
        }
        [RelayCommand]
        async Task signUp()
        {

            player.username = usernameEntry;
            await Application.Current.MainPage.Navigation.PushAsync(new chooseEgg(player));
        }
    }
}
