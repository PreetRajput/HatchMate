using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.apiCalls;
using System.Threading.Tasks;

namespace MauiApp1.viewModel
{
    public partial class chooseEggViewModel: ObservableObject
    {
        public userDetails player;
        public chooseEggViewModel(userDetails player)
        {
            this.player = player;
        }
        [RelayCommand]
        public async Task choosedEgg(string eggBtn)
        {
            player.petNum = int.Parse(eggBtn);
            await Application.Current.MainPage.Navigation.PushAsync(new taskAddition(player));
        }
    }
}
