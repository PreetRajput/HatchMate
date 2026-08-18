using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.BaseClass;
using MauiApp1.Interfaces;
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
        private IPopupService _popUp;

        public signUpPageViewModel(UserDetailsInfoDto userDetails)
        {
            _userDetails = userDetails;
            _popUp = AppService.GetService<IPopupService>();

        }

        [ObservableProperty]
         string usernameEntry;
       
        [RelayCommand]
        async Task signUp()
        {
            try
            {
                _userDetails.Username = usernameEntry;
                var page = ((App)Application.Current).Services.GetRequiredService<chooseEgg>();
                await Application.Current.MainPage.Navigation.PushAsync(page);
            }
            catch (Exception ex)
            {
                await _popUp.OpenUserAddedPopUp("Unhandled Exception", ex.ToString());
            }
        }
    }
}
