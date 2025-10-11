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
            bool exist = Preferences.Get("isLoggedIn", false);
            if (exist)
		        Application.Current.MainPage = new AppShell();
            else
                await Navigation.PushAsync(new loginPage());

        }
    }

}
