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
    public partial class petNameInputViewModel: ObservableObject
    {
        [ObservableProperty]
        string petName;

        userDetails Player;
        public petNameInputViewModel(userDetails Player)
        {
            this.Player = Player;

        }
        [RelayCommand]
        public async void hatchEgg()
        {
            if (string.IsNullOrWhiteSpace(PetName))
            {
                await Application.Current.MainPage.DisplayAlert("error", "A pet name is mandatory", "OK");
            }
            else
            {
                Player.petName = PetName;
                await Application.Current.MainPage.Navigation.PushAsync(new NewPage5(Player));

            }
        }
    }
}
