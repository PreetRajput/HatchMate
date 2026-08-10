using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using models.Dtos.UserDtos;

namespace MauiApp1.viewModel
{
    public partial class chooseEggViewModel: ObservableObject
    {
        private readonly UserDetailsInfoDto _userDetails;
        public chooseEggViewModel(UserDetailsInfoDto userDetails)
        {
            _userDetails = userDetails;
        }
        [RelayCommand]
        public async Task choosedEgg(string eggBtn)
        {
            _userDetails.Pet_Type = eggBtn;
            var page = ((App)Application.Current).Services.GetRequiredService<taskAddition>();
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }
    }
}
