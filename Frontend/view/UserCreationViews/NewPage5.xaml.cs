using MauiApp1.viewModel;
using models.Dtos;
using System.Threading.Tasks;

namespace MauiApp1;

public partial class NewPage5 : ContentPage
{
	public NewPage5(NewPage5ViewModel vm)
	{
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
		(BindingContext as NewPage5ViewModel)?.animatingEggCommand.Execute(null);
    }
}