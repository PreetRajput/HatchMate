using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            var services = ((App)Application.Current!).Services;
            GoalsContent.ContentTemplate = new DataTemplate(() => services.GetRequiredService<Goals>());
            PetContent.ContentTemplate = new DataTemplate(() => services.GetRequiredService<Pet>());
            AppSettingsContent.ContentTemplate = new DataTemplate(() => services.GetRequiredService<AppSettings>());
        }
    }
}
