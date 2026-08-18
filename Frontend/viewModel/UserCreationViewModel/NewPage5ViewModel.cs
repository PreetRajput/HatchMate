using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.BaseClass;
using MauiApp1.Interfaces;
using MauiApp1.Services;
using models.Dtos.UserDtos;


namespace MauiApp1.viewModel
{
    public partial class NewPage5ViewModel: ObservableObject
    {
        [ObservableProperty]
        string source = "egg1.png";

        [ObservableProperty]
        double rotate;

        [ObservableProperty]
        bool visibility= false;

        [ObservableProperty]
        double opacityMeter=1;

        [ObservableProperty]
        double scaling = 1;

        private readonly UserDetailsInfoDto _userDetails;
        private readonly ApiService _apiService;
        private IPopupService _popUp;
        public NewPage5ViewModel(ApiService api, UserDetailsInfoDto userDetails)
        {
            _apiService = api;
            _userDetails = userDetails;
            _popUp = AppService.GetService<IPopupService>();
        }

        [RelayCommand]
        async Task animatingEgg()
        {
            for (int i = 0; i < 5; i++)
            {
                for (global::System.Int32 j = 0; j < 10; j++)
                {
                    Rotate = j;
                    await Task.Delay(8);
                }
                for (global::System.Int32 j = 0; j < 10; j++)
                {
                    Rotate = -j;
                    await Task.Delay(8);
                }

            }
            var wiggle = Task.Run(async () =>
            {
                while (opacityMeter > 0)
                {
                    for (global::System.Int32 j = 0; j < 10; j++)
                    {
                        Rotate = j;
                        await Task.Delay(8);
                    }
                    for (global::System.Int32 j = 0; j < 10; j++)
                    {
                        Rotate = -j;
                        await Task.Delay(8);
                    }
                }
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    Rotate = 0;
                    await Task.Delay(50);
                    Source = "chicklet.png";
                    OpacityMeter = 1;
                    Scaling = 1;
                    await Task.Delay(1000);
                    Visibility = true;
                });

            });
            var scaleTask = Task.Run(async () =>
            {
                for (global::System.Int32 i = 1; i <= 30; i++)
                {
                    Scaling = 1 + (i * 0.1);
                    await Task.Delay(66);
                }
            });
            var fadeTask = Task.Run(async () =>
            {
                for (global::System.Int32 i = 0; i <= 20; i++)
                {
                    opacityMeter = 1 - (i * 0.05);
                    await Task.Delay(100);
                }
            });
            await Task.WhenAll(

                    scaleTask,
                    fadeTask,
                    wiggle

                ); 
        }


        [RelayCommand]
        public async Task navToShell()
        {
            try
            {
                _userDetails.Pet_Level = 1;
                await _apiService.PostUserAsync(_userDetails);
                await _apiService.PostPetAsync(_userDetails);
                await _apiService.PostTaskAsync(_userDetails);
                await _popUp.OpenUserAddedPopUp("UserAdded", "Completion");
                Application.Current.MainPage = new AppShell();
            }
            catch (Exception ex)
            {
                await _popUp.OpenUserAddedPopUp("Unhandled Exception", ex.ToString());
            }
        }
    }
}
