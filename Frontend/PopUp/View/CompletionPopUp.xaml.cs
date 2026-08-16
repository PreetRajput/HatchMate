namespace MauiApp1.PopUp.View;
using CommunityToolkit.Maui.Views;
using MauiApp1.BaseClass;
using MauiApp1.PopUp.ViewModels;

public partial class CompletionPopUp : Popup
{
	public CompletionPopUp()
	{
        CompletionPopUp_ViewModel? vm = BindingContext as CompletionPopUp_ViewModel;
        InitializeComponent();
	}
	public async void OnClickAsync(object sender, EventArgs e)
	{
		await CloseAsync();
	}
}