namespace MauiApp1
{
    public partial class enterPage : ContentPage
    {

        public enterPage()
        {
            InitializeComponent();
        }

        private async void navToBody(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new loginPage());
        }
    }

}
