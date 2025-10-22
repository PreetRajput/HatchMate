using MauiApp1.viewModel;
using Microsoft.Maui.Storage;
namespace MauiApp1;

public partial class AppSettings : ContentPage
{
	public AppSettings()
	{
		InitializeComponent();
		BindingContext= new AppSettingsViewModel();
	}
	
}