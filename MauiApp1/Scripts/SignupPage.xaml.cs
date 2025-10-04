using MauiApp1.apiCalls;

namespace MauiApp1;

public partial class SignupPage : ContentPage
{
    public userDetails player= new userDetails();
	public SignupPage(string email)
	{
        player.id = email;
		InitializeComponent();
	}

    private async void signUp(object sender, EventArgs e)
    {
        var usernameEntry = UsernameEntry.Text;
        player.username = usernameEntry;
            await Navigation.PushAsync(new chooseEgg(player));
    }
}