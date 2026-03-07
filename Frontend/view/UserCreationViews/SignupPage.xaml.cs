using MauiApp1.viewModel;
using models.Dtos;

namespace MauiApp1;

public partial class SignupPage : ContentPage
{

    public SignupPage(signUpPageViewModel viewModel)
	{
        InitializeComponent();
        BindingContext= viewModel;
    }


}