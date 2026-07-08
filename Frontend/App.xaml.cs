 using MauiApp1.Services;
using System.Diagnostics;

namespace MauiApp1
{
    public partial class App : Application
    {
        public IServiceProvider Services { get; set; }
        public  ApiService _apiService {  get; set; }
        public App(IServiceProvider services, ApiService apiService)
        {
            Services = services;
            _apiService = apiService;
            InitializeComponent();
            MainPage = new ContentPage(); // temporary blank page
            _ = InitializeEverything();
        }

        async Task InitializeEverything()
        {
            bool ans = await _apiService.InitializeFromStorageAsync();
            if (ans)
            {
                MainPage = new AppShell();
            }
            else
            {
                var page = Services.GetRequiredService<loginPage>();
                MainPage = new NavigationPage(page);
            }
        }
    }
}
