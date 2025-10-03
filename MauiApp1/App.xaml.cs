namespace MauiApp1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Application.Current.MainPage = new NavigationPage(new enterPage());
        }

    }
}