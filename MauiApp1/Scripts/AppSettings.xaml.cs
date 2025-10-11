using Microsoft.Maui.Storage;
namespace MauiApp1;

public partial class AppSettings : ContentPage
{
	public AppSettings()
	{
		InitializeComponent();
	}
	public void logOut(object sender, EventArgs e)
	{
		Preferences.Remove("email");
		Preferences.Remove("isLoggedIn");
    }
}