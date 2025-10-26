using MauiApp1.apiCalls;
using MauiApp1.viewModel;
using System.Threading.Tasks;

namespace MauiApp1;

public partial class NewPage5 : ContentPage
{
	public NewPage5(userDetails Player)
	{
        InitializeComponent();
        BindingContext = new NewPage5ViewModel(Player);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
		(BindingContext as NewPage5ViewModel)?.animatingEggCommand.Execute(null);
    }
}