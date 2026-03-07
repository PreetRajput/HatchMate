using MauiApp1.viewModel;
using models.Dtos;

namespace MauiApp1;

public partial class petNameInput : ContentPage
{
	public petNameInput(petNameInputViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
	}
    protected override  void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as petNameInputViewModel)?.eggCrumblingCommand.Execute(null);
    }

 
}