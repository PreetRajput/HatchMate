using MauiApp1.apiCalls;

namespace MauiApp1;

public partial class SignupPage : ContentPage
{
    public readonly string email;
	public SignupPage(string email)
	{
        this.email = email;
		InitializeComponent();
	}

    private async void signUp(object sender, EventArgs e)
    {
        var usernameEntry = UsernameEntry.Text;
        var api = new updateData();
        await DisplayAlert("Killer", email, "Ok");
        await DisplayAlert("Killer", usernameEntry, "Ok");

        var person= new userDetails
        {
            id = email,
            username = usernameEntry,
        };
        bool op= await api.updateUserInfo(person);
        if (op)
           await DisplayAlert("Success", "Updates", "Ok");
        else
           await DisplayAlert("Error", "Failed", "OK");

            await Navigation.PushAsync(new chooseEgg());
    }
}