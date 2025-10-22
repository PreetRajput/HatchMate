using MauiApp1.model;
using Microsoft.Maui.Dispatching;

namespace MauiApp1;

public partial class Pet : ContentPage
{
    private bool running = true;
    private int currentAnimationIndex = 2; // start with walk

    private readonly List<List<string>> allAnimation = new()
    {
        new() { "c1.png", "c2.png", "c3.png", "c4.png" }, // cute
        new() { "s1.png", "s2.png" },                     // sleep
        new() { "w1.png", "w2.png", "w3.png", "w4.png" }  // walk
    };

    public Pet()
    {
        InitializeComponent();
        StartAnimationLoop();
    }

    private async void StartAnimationLoop()
    {
        petLabel.Text = detailsPage.currentUser.petName;
        int frameDelay = 100;
        while (running)
        {
            var frames = allAnimation[currentAnimationIndex];
            foreach (var frame in frames)
            {
                animal.Source = frame;
                await Task.Delay(frameDelay);
            }
        }
    }

    private void changeAnimation(object sender, EventArgs e)
    {
        Random random = new Random();
        currentAnimationIndex = random.Next(allAnimation.Count); // pick random animation
    }


  




}
