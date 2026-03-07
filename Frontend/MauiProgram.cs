using MauiApp1.Services;
using MauiApp1.viewModel;
using Microsoft.Extensions.Logging;
using models.Dtos;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace MauiApp1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("CheeseRegular.ttf", "cheeseRegular");
                    fonts.AddFont("bitcount.ttf", "bitcount");
                });

			builder.Logging.AddDebug();

            builder.Services.AddSingleton<ApiService>();
            builder.Services.AddSingleton<AuthApiService>();

            builder.Services.AddTransient<loginPage>();
            builder.Services.AddTransient<SignupPage>();
            builder.Services.AddTransient<chooseEgg>();
            builder.Services.AddTransient<taskAddition>();
            builder.Services.AddTransient<petNameInput>();
            builder.Services.AddTransient<NewPage5>();
            builder.Services.AddTransient<loginPageViewModel>();
            builder.Services.AddTransient<signUpPageViewModel>();
            builder.Services.AddTransient<chooseEggViewModel>();
            builder.Services.AddTransient<GoalsViewModel>();
            builder.Services.AddTransient<taskAdditionViewModel>();
            builder.Services.AddTransient<petNameInputViewModel>();
            builder.Services.AddTransient<NewPage5ViewModel>();
            builder.Services.AddTransient<GoalsViewModel>();
            builder.Services.AddTransient<PetViewModel>();
            builder.Services.AddTransient<AppSettingsViewModel>();

            builder.Services.AddSingleton<PetInfoDto>();
            builder.Services.AddSingleton<UserAppRelatedInfoDto>();
            builder.Services.AddSingleton<TaskListDto>();

            return builder.Build();
        }
    }
}
