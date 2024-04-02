using CommunityToolkit.Maui;
using FinanceMAUI.Repositories;
using FinanceMAUI.Services;
using FinanceMAUI.ViewModels;
using FinanceMAUI.Views;
using Microsoft.Extensions.Logging;

namespace FinanceMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .RegisterRepositories()
                .RegisterServices()
                .RegisterViewModels()
                .RegisterViews();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static MauiAppBuilder RegisterRepositories(this MauiAppBuilder builder)
        {
            var baseurl = DeviceInfo.Platform == DevicePlatform.Android
                    ? "http://10.0.2.2:5280"
                    : "https://localhost:7210";
            builder.Services.AddHttpClient("FinanceTrackerApiClient",
                client =>
                {
                    client.BaseAddress = new Uri(baseurl);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                });

            builder.Services.AddTransient<IUserRepository, UserRepository>();

            return builder;
        }

        private static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();

            return builder;
        }

        private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<UserDetailViewModel>();
            builder.Services.AddTransient<UserIncomesListOverviewViewModel>();

            return builder;
        }

        private static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<UserDetailPage>();
            builder.Services.AddTransient<UserIncomesOverviewPage>();

            return builder;
        }
    }
}
