using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinanceMAUI.Models;
using FinanceMAUI.Services;
using FinanceMAUI.ViewModels.Base;
using FinanceMAUI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinanceMAUI.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        [ObservableProperty]
        private RegisterModel _registerModel;

        [ObservableProperty]
        private LoginModel _loginModel;

        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private bool _isAuthenticated;

        private readonly ClientService _clientService;

        public LoginViewModel(ClientService clientService)
        {
            _clientService = clientService;
            RegisterModel = new();
            LoginModel = new();
            IsAuthenticated = false;
            GetUserNameFromSecuredStorage();
        }

        [RelayCommand]
        private async Task Register()
        {
            await _clientService.Register(RegisterModel);
        }

        [RelayCommand]
        private async Task Login()
        {
            await _clientService.Login(LoginModel);
            GetUserNameFromSecuredStorage();
        }

        [RelayCommand]
        private async Task Logout()
        {
            SecureStorage.Default.Remove("Authentication");
            IsAuthenticated = false;
            UserName = "Guest";
            await Shell.Current.GoToAsync("..");
        }
        
        private async void GetUserNameFromSecuredStorage()
        {
            if (!string.IsNullOrEmpty(UserName) && UserName! != "Guest")
            {
                IsAuthenticated = true;
                return;
            }
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            if (serializedLoginResponseInStorage != null)
            {
                UserName = JsonSerializer.Deserialize<LoginResponseModel>(serializedLoginResponseInStorage)!.UserName!;
                IsAuthenticated = true;
                return;
            }
            UserName = "Guest";
            IsAuthenticated = false;
        }


        [RelayCommand]
        private async Task GoToWeatherForecast()
        {
            await Shell.Current.GoToAsync(nameof(WeatherForecastPage));
        }
    }
}
