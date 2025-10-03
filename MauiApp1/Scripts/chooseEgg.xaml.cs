namespace MauiApp1;

public partial class chooseEgg : ContentPage
{
	public chooseEgg()
	{
		InitializeComponent();
	}
	public async void  choosedEgg(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new taskAddition());
    }
}