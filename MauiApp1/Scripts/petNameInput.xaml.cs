using MauiApp1.apiCalls;

namespace MauiApp1;

public partial class petNameInput : ContentPage
{
    userDetails Player;
	public petNameInput(userDetails Player)
	{
        this.Player = Player;
		InitializeComponent();
		eggCrumbling();
	}
	async void eggCrumbling()
	{
        while (true)
        {
            await eggUnhatched.RotateTo(10, 100);
            await eggUnhatched.TranslateTo(5, 0, 100);
            await eggUnhatched.RotateTo(-10, 100);
            await eggUnhatched.TranslateTo(-5, 0, 100);
            await eggUnhatched.RotateTo(0, 100);
            await eggUnhatched.TranslateTo(0, 0, 100);
        }
    }
    public async void hatchEgg(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(petName.Text))
        {
            await DisplayAlert("error", "A pet name is mandatory", "OK");
        }
        else
        {
            Player.petName = petName.Text;
            await Navigation.PushAsync(new NewPage5(Player));

        }
    }
}