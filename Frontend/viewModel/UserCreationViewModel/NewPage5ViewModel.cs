using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Services;
using models.Dtos.PetDtos;
using models.Dtos.TaskDtos;
using models.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private readonly UserAppRelatedInfoDto Player;
        private readonly ApiService _apiService;
        private readonly TaskListDto _tasks;
        private readonly PetInfoDto pet;
      
        public NewPage5ViewModel(ApiService api, UserAppRelatedInfoDto Player, TaskListDto tasks, PetInfoDto pet)
        {
            _tasks = tasks;
            _apiService = api;
            this.Player = Player;
            this.pet = pet;
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
            pet.Pet_Level = 1;
            bool success = await _apiService.PostUserAsync(Player);
            bool petSuccess = await _apiService.PostPetAsync(pet);

            foreach (var abc in _tasks.Tasks)
            {
                Debug.WriteLine("goal hai",abc);
            }
             await _apiService.PostTaskAsync(_tasks);

            if (success && petSuccess)
            {
                    await Application.Current.MainPage.DisplayAlert("Success", "User added", "OK");
                Application.Current.MainPage = new AppShell();

            }
            else
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to add user or pet", "OK");

        }
    }
}
