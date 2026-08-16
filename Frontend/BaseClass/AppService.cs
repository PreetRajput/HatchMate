using MauiApp1.Interfaces;

namespace MauiApp1.BaseClass
{
    public static class AppService 
    {
        static IServiceProvider? _services;
        public static void Setup(IServiceProvider services)
        {
            _services = services;
        }
        public static T GetService<T>()
        {
            return (T)_services.GetRequiredService<T>();
        }
    }
}
