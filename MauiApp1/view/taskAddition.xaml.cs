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
        AnimateImage();
        BindingContext= new taskAdditionViewModel(Player);
    }
    async void AnimateImage()
    {
        while (true)
        {
            // Rotate around Y-axis (pseudo-3D)
            await selectedEgg.RotateYTo(360, 2000);  // 360 degrees in 2 seconds
            selectedEgg.RotationY = 0;               // reset to start
        }
    }
 
   

}