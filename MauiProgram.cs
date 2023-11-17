using HikeTracker.DB;
using HikeTracker.Service;
using HikeTracker.View;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace HikeTracker
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseMauiCommunityToolkit();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<ObservationPage>();
            builder.Services.AddSingleton<AddHike>();
            builder.Services.AddSingleton<HikeViewModel>();
            builder.Services.AddSingleton<ObservationViewModel>();
            builder.Services.AddSingleton<AddHikeViewModel>();
            builder.Services.AddSingleton<HikeService>();
            builder.Services.AddSingleton<ObservationService>();
            builder.Services.AddSingleton<HikeDB>();
            builder.Services.AddSingleton<ObservationDB>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<ObservationPage>();
            builder.Services.AddTransient<AddHike>();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}