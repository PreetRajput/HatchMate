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
    protected override void OnAppearing()
    {

        base.OnAppearing();
        

        (BindingContext as PetViewModel)?.getPetNameCommand.Execute(null);

    }

    




}
