using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.BaseClass;
using MauiApp1.Interfaces;
using models.Dtos.UserDtos;

namespace MauiApp1.viewModel
{
    public partial class chooseEggViewModel: ObservableObject
    {
        private readonly UserDetailsInfoDto _userDetails;
        private IPopupService _popUp;

        public chooseEggViewModel(UserDetailsInfoDto userDetails)
        {
            _userDetails = userDetails;
            _popUp = AppService.GetService<IPopupService>();

        }
        [RelayCommand]
        public async Task choosedEgg(string eggBtn)
        {
            try
            {
                _userDetails.Pet_Type = eggBtn;
                var page = ((App)Application.Current).Services.GetRequiredService<taskAddition>();
                await Application.Current.MainPage.Navigation.PushAsync(page);
            }
            catch (Exception ex)
            {
                await _popUp.OpenUserAddedPopUp("Unhandled Exception", ex.ToString());
            }
        }
    }
}
