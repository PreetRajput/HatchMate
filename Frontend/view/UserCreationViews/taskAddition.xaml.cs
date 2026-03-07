using MauiApp1.viewModel;
using Microsoft.Maui.Controls.Shapes;
using models.Dtos;
using System.Drawing;
using System.Globalization;

namespace MauiApp1;

public partial class taskAddition : ContentPage
{
    public taskAddition(taskAdditionViewModel vm)
	{
		InitializeComponent();
        BindingContext= vm;

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as taskAdditionViewModel)?.AnimateImageCommand.Execute(null);
    }
    
 
   

}