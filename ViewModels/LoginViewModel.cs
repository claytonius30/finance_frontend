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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

        //[ObservableProperty]
        //private RegisterModel _registerModel;

        //[ObservableProperty]
        //private LoginModel _loginModel;

        [ObservableProperty]
        private bool _isAuthenticated;

        [ObservableProperty]
        private string _userName;

        [Required]
        [EmailAddress]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _emailRegister;

        //[Required]
        //[EmailAddress]
        //[NotifyDataErrorInfo]
        [ObservableProperty]
        private string _emailLogin;

        [Required]
        [PasswordPropertyText]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _passwordRegister;

        //[Required]
        //[PasswordPropertyText]
        //[NotifyDataErrorInfo]
        [ObservableProperty]
        private string _passwordLogin;

        [MinLength(1)]
        [MaxLength(50)]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _firstName;

        [MinLength(1)]
        [MaxLength(50)]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _lastName;

        [ObservableProperty]
        private bool _toggleRegister = false;

        [ObservableProperty]
        private bool _toggleLogin = true;
        
        public ObservableCollection<ValidationResult> Errors { get; } = new();
        
        public LoginViewModel(ClientService clientService, IUserService userService, INavigationService navigationService, IDialogService dialogService)
        {
            _clientService = clientService;
            _userService = userService;
            _navigationService = navigationService;
            _dialogService = dialogService;
            //RegisterModel = new();
            //LoginModel = new();
            IsAuthenticated = false;
            GetUserNameFromSecuredStorage();
            
            WeakReferenceMessenger.Default.Register(this);

            ErrorsChanged += CreateUserViewModel_ErrorsChanged;
            ErrorsChanged += LoginUserViewModel_ErrorsChanged;
        }

        [RelayCommand]
        private void SignUpLabel()
        {
            (ToggleRegister, ToggleLogin) = (ToggleLogin, ToggleRegister);
        }

        [RelayCommand(CanExecute = nameof(CanRegisterUser))]
        private async Task Register()
        {
            ValidateAllProperties();
            if (Errors.Any())
            {
                return;
            }

        RegisterModel registerModel = MapDataToRegisterModel();
            
            await _clientService.Register(registerModel);

            Guid userId = await _userService.GetGuid(EmailRegister);

            UserModel? newUser = await _userService.GetUser(userId);

            if (newUser != null)
            {
                if (FirstName == null && LastName == null)
                {
                    newUser.FirstName = EmailRegister;
                }
                newUser.FirstName = FirstName!;
                newUser.LastName = LastName!;
                if (await _userService.PutUser(newUser))
                {
                    WeakReferenceMessenger.Default.Send(new UserCreatedMessage());
                    await _dialogService.Notify("Success", "You have been registered.");
                    await _navigationService.GoToUserDetail(userId);
                }
                else
                {
                    await _dialogService.Notify("Failed", "User Registration failed.");
                }
            }
            else
            {
                await _dialogService.Notify("Failed", "User Registration failed.");
            }
        }

        private bool CanRegisterUser() => !HasErrors;

        [RelayCommand(CanExecute = nameof(CanLoginUser))]
        private async Task Login()
        {
            //ValidateAllProperties();
            if (Errors.Any())
            {
                return;
            }

            LoginModel loginModel = MapDataToLoginModel();

            //LoginModel.Email = Email;
            //LoginModel.Password = Password;
            await _clientService.Login(loginModel);
            GetUserNameFromSecuredStorage();
            if (IsAuthenticated)
            {
                Guid userId = await _userService.GetGuid(UserName);
                WeakReferenceMessenger.Default.Send(new LoginMessage());
                //await _dialogService.Notify("Success", "You are logged in.");
                await _navigationService.GoToUserDetail(userId);
            }
            else
            {
                await _dialogService.Notify("Failed", "Login failed.");
            }
        }

        private bool CanLoginUser() => !HasErrors;

        //[RelayCommand]
        private void Logout()
        {
            SecureStorage.Default.Remove("Authentication");
        }


        private RegisterModel MapDataToRegisterModel()
        {
            return new RegisterModel
            {
                Email = EmailRegister,
                Password = PasswordRegister
            };
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
            if (!string.IsNullOrEmpty(UserName) && UserName! != "guest")
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
            IsAuthenticated = false;
        }

        private void CreateUserViewModel_ErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            Errors.Clear();
            GetErrors().ToList().ForEach(Errors.Add);
            RegisterCommand.NotifyCanExecuteChanged();
        }
        private void LoginUserViewModel_ErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            Errors.Clear();
            GetErrors().ToList().ForEach(Errors.Add);
            LoginCommand.NotifyCanExecuteChanged();
        }

        public void Receive(LogoutMessage message)
        {
            Logout();
            ToggleRegister = false;
            ToggleLogin = true;
            IsAuthenticated = false;
            PasswordLogin = "";
        }

        //[RelayCommand]
        //private async Task GoToWeatherForecast()
        //{
        //    await Shell.Current.GoToAsync(nameof(WeatherForecastPage));
        //}
    }
}
