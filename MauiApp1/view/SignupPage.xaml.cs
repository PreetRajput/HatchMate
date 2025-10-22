using MauiApp1.apiCalls;
using MauiApp1.viewModel;

namespace MauiApp1;

public partial class SignupPage : ContentPage
{
    public userDetails player= new userDetails();
	public SignupPage(string email)
	{
        player.id = email;
		InitializeComponent();
        BindingContext= new signUpPageViewModel(player);
    }

  
}