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
                    fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIconsRegular");
                })
                .RegisterRepositories()
                .RegisterServices()
                .RegisterViewModels()
                .RegisterViews();

            builder.Services.AddSingleton<IPlatformHttpMessageHandler>(sp =>
            {
#if ANDROID
                return new AndroidHttpMessageHandler();
#else
                return null!;
#endif
            });


#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        [Obsolete]
        private static MauiAppBuilder RegisterRepositories(this MauiAppBuilder builder)
        {
            var baseurl = DeviceInfo.Platform == DevicePlatform.Android
                    //? "http://10.0.2.2:5280"
                    //: "https://localhost:7210";
                    ? "http://10.0.2.2:7208"
                    : "https://localhost:7208";
            builder.Services.AddHttpClient("FinanceTrackerApiClient",
                client =>
                {
                    client.BaseAddress = new Uri(baseurl);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                }).ConfigureHttpMessageHandlerBuilder(configureBuilder =>
                {
                    var platformMessageHandler = configureBuilder.Services.GetRequiredService<IPlatformHttpMessageHandler>();
                    configureBuilder.PrimaryHandler = platformMessageHandler.GetHttpMessageHandler();
                });

            builder.Services.AddSingleton<MainPage>();

            builder.Services.AddTransient<IUserRepository, UserRepository>();

            return builder;
        }

        private static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<IDialogService, DialogService>();

            builder.Services.AddSingleton<ClientService>();

            return builder;
        }

        private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<UserDetailViewModel>();
            builder.Services.AddTransient<UserIncomesListOverviewViewModel>();
            builder.Services.AddTransient<IncomeAddEditViewModel>();
            builder.Services.AddTransient<IncomeDetailViewModel>();

            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<WeatherForecastViewModel>();

            return builder;
        }

        private static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<UserDetailPage>();
            builder.Services.AddTransient<UserIncomesOverviewPage>();
            builder.Services.AddTransient<IncomeAddEditPage>();
            builder.Services.AddTransient<IncomeDetailPage>();

            builder.Services.AddSingleton<WeatherForecastPage>();

            return builder;
        }
    }
}
