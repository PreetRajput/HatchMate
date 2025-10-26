using MauiApp1.model;
using MauiApp1.viewModel;
using Microsoft.Maui.Dispatching;

namespace MauiApp1;

public partial class Pet : ContentPage
{
  

        bool hasRun = false;
    public Pet()
    {
        InitializeComponent();
        BindingContext = new PetViewModel();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing(); 
        if (!hasRun)
        {
            (BindingContext as PetViewModel)?.StartAnimationLoopCommand.Execute(null);
            hasRun = true;
        }
    }

  




}
