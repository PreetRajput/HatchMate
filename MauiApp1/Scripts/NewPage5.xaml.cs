namespace MauiApp1;

public partial class NewPage5 : ContentPage
{
	public NewPage5()
	{
		InitializeComponent();
		animatingEgg();
	}
	public async void animatingEgg()
	{
        for (int i = 0; i < 5; i++)
        {
			await egg.RotateTo(10, 80);
			await egg.RotateTo(-10, 80);
			
        }
		var wiggle = Task.Run(async () =>
		{
			while(egg.Opacity>0)
			{
                await egg.RotateTo(10, 80);
                await egg.RotateTo(-10, 80);
            }
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await egg.RotateTo(0, 50);
                egg.Source = "chicklet.png";
				egg.Opacity = 1;
				egg.Scale = 1;
				await Task.Delay(1000);
				growBtn.IsVisible = true;
            });

        });
		await Task.WhenAll(
				egg.ScaleTo(3, 2000, Easing.CubicIn),
				egg.FadeTo(0,2000, Easing.Linear),
				wiggle
					
			); 
    }
	public void navToShell(object sender, EventArgs e)
	{
		Application.Current.MainPage = new AppShell();
	}
}