﻿// Clayton DeSimone
// .NET Applications
// Final Project
// 4/29/2024

using CommunityToolkit.Maui;
using FinanceMAUI.Repositories;
using FinanceMAUI.Services;
using FinanceMAUI.ViewModels;
using FinanceMAUI.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FinanceMAUI
{
    public static class MauiProgram
    {
        [Obsolete]
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
            
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }

        [Obsolete]
        private static MauiAppBuilder RegisterRepositories(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<IPlatformHttpMessageHandler>(sp =>
            {
#if ANDROID
                return new AndroidHttpMessageHandler();
#elif WINDOWS
                return new WindowsHttpMessageHandler();
#else
                return null!;
#endif
            });

            var baseurl = DeviceInfo.Platform == DevicePlatform.Android
                    ? "https://10.0.2.2:7208"
                    : "https://localhost:7208";
            builder.Services.AddHttpClient("custom-httpclient",
                client =>
                {
                    client.BaseAddress = new Uri(baseurl);
                }).ConfigureHttpMessageHandlerBuilder(configureBuilder =>
                {
                    var platformMessageHandler = configureBuilder.Services.GetRequiredService<IPlatformHttpMessageHandler>();
                    configureBuilder.PrimaryHandler = platformMessageHandler.GetHttpMessageHandler();
                });
            
            builder.Services.AddTransient<IUserRepository, UserRepository>();

            return builder;
        }

        private static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<IDialogService, DialogService>();

            builder.Services.AddSingleton<IClientService, ClientService>();

            return builder;
        }

        private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddSingleton<UserDetailViewModel>();
            builder.Services.AddTransient<UserIncomesListOverviewViewModel>();
            builder.Services.AddTransient<IncomeAddEditViewModel>();
            builder.Services.AddTransient<IncomeDetailViewModel>();
            builder.Services.AddTransient<UserExpensesListOverviewViewModel>();
            builder.Services.AddTransient<ExpenseAddEditViewModel>();
            builder.Services.AddTransient<ExpenseDetailViewModel>();
            builder.Services.AddTransient<TransactionsListOverviewViewModel>();
            builder.Services.AddTransient<GoalsListOverviewViewModel>();
            builder.Services.AddTransient<GoalDetailViewModel>();
            builder.Services.AddTransient<GoalAddEditViewModel>();

            return builder;
        }

        private static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddSingleton<UserDetailPage>();
            builder.Services.AddTransient<UserIncomesOverviewPage>();
            builder.Services.AddTransient<IncomeAddEditPage>();
            builder.Services.AddTransient<IncomeDetailPage>();
            builder.Services.AddTransient<UserExpensesOverviewPage>();
            builder.Services.AddTransient<ExpenseAddEditPage>();
            builder.Services.AddTransient<ExpenseDetailPage>();
            builder.Services.AddTransient<TransactionsOverviewPage>();
            builder.Services.AddTransient<GoalsOverviewPage>();
            builder.Services.AddTransient<GoalDetailPage>();
            builder.Services.AddTransient<GoalAddEditPage>();

            return builder;
        }
    }
}
