using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.viewModel
{
    public partial class PetViewModel: ObservableObject
    {
        [ObservableProperty]
        string labelText;

        [ObservableProperty]
        string imageSource = "w1.png";

        private bool running = true;
        private int currentAnimationIndex = 2; // start with walk

        private readonly List<List<string>> allAnimation = new()
    {
        new() { "c1.png", "c2.png", "c3.png", "c4.png" }, // cute
        new() { "s1.png", "s2.png" },                     // sleep
        new() { "w1.png", "w2.png", "w3.png", "w4.png" }  // walk
    };
        [RelayCommand]
         async void StartAnimationLoop()
        {
            LabelText = detailsPage.currentUser.petName;
            int frameDelay = 100;
            while (running)
            {
                var frames = allAnimation[currentAnimationIndex];
                foreach (var frame in frames)
                {
                    ImageSource = frame;
                    await Task.Delay(frameDelay);
                }
            }
        }

        [RelayCommand]
        void changeAnimation()
        {
            Random random = new Random();
            currentAnimationIndex = random.Next(allAnimation.Count); // pick random animation
        }


    }
}
