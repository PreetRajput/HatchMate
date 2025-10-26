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

        userDetails Player;
        public NewPage5ViewModel(userDetails Player)
        {
            this.Player = Player;
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
            foreach (string item in Player.tasks)
            {
                await Application.Current.MainPage.DisplayAlert("error", $"{item}", "OK");

            }

            var api = new updateWhole();
            bool success = await api.updateDocument(Player);

            if (success)
                await Application.Current.MainPage.DisplayAlert("Success", "User added", "OK");
            else
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to add user", "OK");

            Application.Current.MainPage = new AppShell();
        }
    }
}
