namespace MauiApp1;

public partial class petNameInput : ContentPage
{
	public petNameInput()
	{
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
        await Navigation.PushAsync(new NewPage5());
    }
}