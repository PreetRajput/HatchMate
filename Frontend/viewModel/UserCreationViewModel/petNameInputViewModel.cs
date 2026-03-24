using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using models.Dtos.PetDtos;
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
        [ObservableProperty]
        double rotate;
        [ObservableProperty]
        double translate;

        public PetInfoDto pet;
        public petNameInputViewModel(PetInfoDto pet)
        {
            this.pet = pet;

        }
        
        [RelayCommand]
        async Task eggCrumbling()
        {
            while (true)
            {
                for (global::System.Int32 i = 0; i < 10; i++)
                {
                    Rotate = i;
                    await Task.Delay(10);
                }
                for (global::System.Int32 i = 0; i < 10; i++)
                {
                    translate = i / 2;
                    await Task.Delay(10);
                }
                for (global::System.Int32 i = 0; i < 10; i++)
                {
                    Rotate = -i;
                    await Task.Delay(10);
                }
                for (global::System.Int32 i = 0; i < 10; i++)
                {
                    Translate = -i / 2;
                    await Task.Delay(10);
                }
                for (global::System.Int32 i = 10; i > 0; i--)
                {
                    Rotate = -i;
                    await Task.Delay(10);
                }
                for (global::System.Int32 i = 10; i > 0; i--)
                {
                    Translate = -i / 2;
                    await Task.Delay(10);
                }
                   
            }
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
                    pet.PetName = PetName;
                var page = ((App)Application.Current).Services.GetRequiredService<NewPage5>();
                await Application.Current.MainPage.Navigation.PushAsync(page);

            }
        }
    }
}
