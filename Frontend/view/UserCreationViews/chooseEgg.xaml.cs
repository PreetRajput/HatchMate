using MauiApp1.viewModel;
using models.Dtos;

namespace MauiApp1;

public partial class chooseEgg : ContentPage
{
	public chooseEgg(chooseEggViewModel viewModel)
	{
		InitializeComponent();
		BindingContext	= viewModel;
    }
	
}