using MauiApp1.apiCalls;
using MauiApp1.viewModel;

namespace MauiApp1;

public partial class chooseEgg : ContentPage
{
	public chooseEgg(userDetails player)
	{
		InitializeComponent();
		BindingContext	= new chooseEggViewModel(player);
    }
	
}