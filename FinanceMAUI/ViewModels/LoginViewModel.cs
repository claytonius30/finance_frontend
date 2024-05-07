// Clayton DeSimone
// .NET Applications
// Final Project
// 4/29/2024

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FinanceMAUI.Messages;
using FinanceMAUI.Models;
using FinanceMAUI.Services;
using FinanceMAUI.ViewModels.Base;
using FinanceMAUI.Views;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinanceMAUI.ViewModels
{
    public partial class LoginViewModel : ViewModelBase, IRecipient<LogoutMessage>, IRecipient<LoginMessage>
    {
        private readonly IClientService _clientService;
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        [ObservableProperty]
        private bool _isAuthenticated;
        
        private bool failedAttempt;

        [Required]
        [EmailAddress]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _userName;

        [Required]
        [PasswordPropertyText]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _passwordLogin;
        
        [ObservableProperty]
        private bool _toggleRegister = false;

        [ObservableProperty]
        private bool _toggleLogin = true;

        public ObservableCollection<ValidationResult> LoginErrors { get; } = new();

        public LoginViewModel(IClientService clientService, IUserService userService, INavigationService navigationService, IDialogService dialogService)
        {
            _clientService = clientService;
            _userService = userService;
            _navigationService = navigationService;
            _dialogService = dialogService;

            IsAuthenticated = false;
            failedAttempt = false;

            WeakReferenceMessenger.Default.Register<LogoutMessage>(this);
            WeakReferenceMessenger.Default.Register<LoginMessage>(this);
        
            ErrorsChanged += LoginUserViewModel_ErrorsChanged;
        }
        
        [RelayCommand]
        private async Task SignUpLabel()
        {
            await _navigationService.GoToRegister();
        }
        
        [RelayCommand(CanExecute = nameof(CanLoginUser))]
        private async Task Login()
        {
            failedAttempt = false;
            ValidateAllProperties();
            if (LoginErrors.Any())
            {
                return;
            }

            LockoutInfoModel lockoutEnd = await _userService.GetLockoutEnd(UserName);
            LoginModel loginModel = MapDataToLoginModel();

            await _clientService.Login(loginModel);
            if (failedAttempt == false)
            {
                GetUserNameFromSecuredStorage();
            }
            else if (lockoutEnd != null && !lockoutEnd.LockoutRemaining!.Equals(""))
            {
                await _dialogService.Notify("Account Locked", $"Lockout ends {lockoutEnd.LockoutEnd}\n{lockoutEnd.LockoutRemaining}");
                IsAuthenticated = false;
                return;
            }
            else
            {
                await _dialogService.Notify("Failed", "Email or Password incorrect.");
                return;
            }
            if (!lockoutEnd.LockoutRemaining!.Equals(""))
            {
                await _dialogService.Notify("Account Locked", $"Lockout ends {lockoutEnd.LockoutEnd}\n{lockoutEnd.LockoutRemaining}");
                IsAuthenticated = false;
                return;
            }
            if (IsAuthenticated)
            {
                Guid userId = await _userService.GetGuid(UserName);
                await _navigationService.GoToUserDetail(userId);
            }
            else
            {
                await _dialogService.Notify("Failed", "Login Failed.");
            }
        }

        private bool CanLoginUser() => !HasErrors;
        
        private async Task Logout()
        {
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            var serializedLogoutResponseInStorage = await SecureStorage.Default.GetAsync("RegPW");
            string tempUserName = JsonSerializer.Deserialize<LoginResponseModel>(serializedLoginResponseInStorage!)!.UserName!;
            if (tempUserName == UserName && serializedLogoutResponseInStorage != PasswordLogin)
            {
                SecureStorage.Default.Remove("Authentication");
                IsAuthenticated = false;
            }
            else
            {
                UserName = tempUserName;
                PasswordLogin = JsonSerializer.Deserialize<string>(serializedLogoutResponseInStorage!)!;
            }
        }

        private LoginModel MapDataToLoginModel()
        {
            return new LoginModel
            {
                Email = UserName,
                Password = PasswordLogin
            };
        }

        private async void GetUserNameFromSecuredStorage()
        {
            if (!string.IsNullOrEmpty(UserName) && failedAttempt == false)
            {
                IsAuthenticated = true;
                return;
            }
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            if (serializedLoginResponseInStorage != null && failedAttempt == false)
            {
                UserName = JsonSerializer.Deserialize<LoginResponseModel>(serializedLoginResponseInStorage)!.UserName!;
                IsAuthenticated = true;
                return;
            }
            IsAuthenticated = false;
        }

        private void LoginUserViewModel_ErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            LoginErrors.Clear();
            GetErrors().ToList().ForEach(LoginErrors.Add);
            LoginCommand.NotifyCanExecuteChanged();
        }

        public async void Receive(LogoutMessage message)
        {
            await Logout();
            await _dialogService.Notify("Success", "You have been logged out.");

        }

        public void Receive(LoginMessage message)
        {
            failedAttempt = true;
        }
    }
}
