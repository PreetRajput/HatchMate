using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.viewModel
{
    public partial class signUpPageViewModel: ObservableObject
    {
        public readonly UserAppRelatedInfoDto _player;
        public signUpPageViewModel(UserAppRelatedInfoDto player)
        {
            _player = player;
        }

        [ObservableProperty]
         string usernameEntry;
       
        [RelayCommand]
        async Task signUp()
        {

            _player.Username = usernameEntry;
            var page = ((App)Application.Current).Services.GetRequiredService<chooseEgg>();
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }
    }
}
