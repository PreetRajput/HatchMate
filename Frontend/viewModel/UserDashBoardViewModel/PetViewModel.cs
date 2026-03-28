using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Services;
using models.Dtos.PetDtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.viewModel
{
    public partial class PetViewModel: ObservableObject
    {
        bool start;
        [ObservableProperty]
        string labelText = "";

        [ObservableProperty]
        string imageSource = "";


        private bool running = true;
        private int currentAnimationIndex = 2; // start with walk

        private readonly List<List<string>> allAnimation = new()
        {
            new() { "c1.png", "c2.png", "c3.png", "c4.png" }, // cute
            new() { "s1.png", "s2.png" },                     // sleep
            new() { "w1.png", "w2.png", "w3.png", "w4.png" }  // walk
        };

        private readonly ApiService _apiService;

        public PetViewModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [RelayCommand]
        public async void getPetName()
        {
            Debug.WriteLine("GETPETNAME command run");
            if (start)
                return;
            var pet = await _apiService.GetPetAsync();
            if (pet != null)
                LabelText = pet.PetName;
            PetDto dto = new()
            {
                Pet_Type = pet.Pet_Type,
                Pet_Level = pet.Pet_Level

            };
            var Animation = await _apiService.GetAnimationAsync(dto);
            
            start = true;
        }

        [RelayCommand]
        void changeAnimation()
        {
            Random random = new Random();
            currentAnimationIndex = random.Next(allAnimation.Count); // pick random animation
        }

        void IdleAnimation()
        {
            foreach(var animation in allAnimation)
        }


    }
}
