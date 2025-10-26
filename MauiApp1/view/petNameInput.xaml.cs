using MauiApp1.apiCalls;
using MauiApp1.viewModel;

namespace MauiApp1;

public partial class petNameInput : ContentPage
{
	public petNameInput(userDetails Player)
	{
        BindingContext = new petNameInputViewModel(Player);
		InitializeComponent();
	}
    protected override  void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as petNameInputViewModel)?.eggCrumblingCommand.Execute(null);
    }
	
  
}