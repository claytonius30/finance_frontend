using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FinanceMAUI.Messages;
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
    public partial class LoginViewModel : ViewModelBase, IRecipient<LogoutMessage>
    {
        private readonly ClientService _clientService;
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        [ObservableProperty]
        private RegisterModel _registerModel;

        [ObservableProperty]
        private LoginModel _loginModel;

        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private bool _isAuthenticated;

        public LoginViewModel(ClientService clientService, IUserService userService, INavigationService navigationService, IDialogService dialogService)
        {
            _clientService = clientService;
            _userService = userService;
            _navigationService = navigationService;
            RegisterModel = new();
            LoginModel = new();
            IsAuthenticated = false;
            GetUserNameFromSecuredStorage();
            _dialogService = dialogService;
            WeakReferenceMessenger.Default.Register(this);
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
            if (IsAuthenticated)
            {
                Guid userId = await _userService.GetGuid(UserName);
                WeakReferenceMessenger.Default.Send(new LoginMessage());
                await _dialogService.Notify("Success", "You are logged in.");
                await _navigationService.GoToUserDetail(userId);
            }
        }

        //[RelayCommand]
        private void Logout()
        {
            SecureStorage.Default.Remove("Authentication");
            IsAuthenticated = false;
            UserName = "Guest";
            //await Shell.Current.GoToAsync("..");
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

        public void Receive(LogoutMessage message)
        {
            Logout();
        }
    }
}
