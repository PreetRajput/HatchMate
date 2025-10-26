using MauiApp1.apiCalls;
using MauiApp1.viewModel;
using Microsoft.Maui.Controls.Shapes;
using System.Drawing;
using System.Globalization;

namespace MauiApp1;

public partial class taskAddition : ContentPage
{
    public taskAddition(userDetails Player)
	{
		InitializeComponent();
        BindingContext= new taskAdditionViewModel(Player);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as taskAdditionViewModel)?.AnimateImageCommand.Execute(null);
    }
    
 
   

}