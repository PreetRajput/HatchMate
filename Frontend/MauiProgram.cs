using HatchMate.Api;
using MauiApp1.Services;
using MauiApp1.Services.Generated;
using MauiApp1.viewModel;
using Microsoft.Extensions.Logging;
using models.Dtos.UserDtos;
using SkiaSharp.Views.Maui.Controls.Hosting;
using CommunityToolkit.Maui;
using MauiApp1.BaseClass;
using MauiApp1.PopUp.ViewModels;
using MauiApp1.PopUp;
using IPopupService = MauiApp1.Interfaces.IPopupService;

namespace MauiApp1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().UseSkiaSharp().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("CheeseRegular.ttf", "cheeseRegular");
                fonts.AddFont("bitcount.ttf", "bitcount");
            }).UseMauiCommunityToolkit();
            builder.Services.AddSingleton<IPopupService, PopUpService>();
            builder.Services.AddSingleton<ApiService>();
            builder.Services.AddSingleton<AuthApiService>();
            builder.Services.AddSingleton<UserDetailsInfoDto>();
            builder.Services.AddTransient<AuthHandler>();

            builder.Services.AddHttpClient<IAuthClient, AuthClient>(client => client.BaseAddress = new Uri("http://192.168.1.4:5000/")).AddHttpMessageHandler<AuthHandler>();
            builder.Services.AddHttpClient<IPetClient, PetClient>(client => client.BaseAddress = new Uri("http://192.168.1.4:5000/")).AddHttpMessageHandler<AuthHandler>();
            builder.Services.AddHttpClient<ISeedClient, SeedClient>(client => client.BaseAddress = new Uri("http://192.168.1.4:5000/")).AddHttpMessageHandler<AuthHandler>();
            builder.Services.AddHttpClient<ITaskClient, TaskClient>(client => client.BaseAddress = new Uri("http://192.168.1.4:5000/")).AddHttpMessageHandler<AuthHandler>();
            builder.Services.AddHttpClient<IUsersClient, UsersClient>(client => client.BaseAddress = new Uri("http://192.168.1.4:5000/")).AddHttpMessageHandler<AuthHandler>();
            
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
            builder.Services.AddTransient<PetViewModel>();
            builder.Services.AddTransient<AppSettingsViewModel>();
            builder.Services.AddTransient<CompletionPopUp_ViewModel>();
            var App = builder.Build();
            AppService.Setup(App.Services);
            return App;
        }
    }
}