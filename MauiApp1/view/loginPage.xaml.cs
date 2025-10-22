using Microsoft.Maui.Authentication;
using Microsoft.Maui.Storage;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using MauiApp1.apiCalls;
using MauiApp1.viewModel;
namespace MauiApp1;

public partial class loginPage : ContentPage
{
	public loginPage()
	{
		InitializeComponent();
		BindingContext= new loginPageViewModel();
    }
   

  


}