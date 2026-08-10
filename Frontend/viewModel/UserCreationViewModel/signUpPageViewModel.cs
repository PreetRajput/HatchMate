using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using models.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.viewModel
{
    public partial class signUpPageViewModel: ObservableObject
    {
        private readonly UserDetailsInfoDto _userDetails;
        public signUpPageViewModel(UserDetailsInfoDto userDetails)
        {
            _userDetails = userDetails;
        }

        [ObservableProperty]
         string usernameEntry;
       
        [RelayCommand]
        async Task signUp()
        {

            _userDetails.Username = usernameEntry;
            var page = ((App)Application.Current).Services.GetRequiredService<chooseEgg>();
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }
    }
}
