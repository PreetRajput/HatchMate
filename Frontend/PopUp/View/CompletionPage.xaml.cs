namespace MauiApp1.PopUp.View;

using CommunityToolkit.Maui.Views;

public partial class CompletionPage : Popup
{
	public CompletionPage()
	{
        InitializeComponent();
	}
	public void OnOkClicked(EventArgs e)
	{
		CloseAsync();
	}
}