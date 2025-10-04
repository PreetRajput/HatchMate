using MauiApp1.apiCalls;

namespace MauiApp1;

public partial class chooseEgg : ContentPage
{
	userDetails player;
	public chooseEgg(userDetails player)
	{
		this.player = player;
		InitializeComponent();
	}
	public async void  choosedEgg(object sender, EventArgs e)
	{
        Button button= (Button)sender;
		player.petNum = int.Parse(button.ClassId);
		await Navigation.PushAsync(new taskAddition(player));
    }
}