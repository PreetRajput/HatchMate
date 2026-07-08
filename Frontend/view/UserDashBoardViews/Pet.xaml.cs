using MauiApp1.viewModel;
using Microsoft.Maui.Dispatching;

namespace MauiApp1;

public partial class Pet : ContentPage
{
    public Pet(PetViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    bool _isInitialized = false;
    protected override void OnAppearing()
    {

        base.OnAppearing();
        if (_isInitialized) return;
        (BindingContext as PetViewModel)?.GetEmotesCommand.Execute(null);
        _isInitialized = true;
    }

    




}
