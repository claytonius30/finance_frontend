using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FinanceMAUI.Messages;
using FinanceMAUI.Models;
using FinanceMAUI.Services;
using FinanceMAUI.ViewModels.Base;
using FinanceMAUI.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinanceMAUI.ViewModels
{
    public partial class LoginViewModel : ViewModelBase, IRecipient<LogoutMessage>, IRecipient<LoginMessage>
    {
        private readonly ClientService _clientService;
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        [ObservableProperty]
        private bool _isAuthenticated;
        
        private bool failedAttempt;
        private int loginAttempts;

        [Required]
        [EmailAddress]
        //[CustomValidation(typeof(LoginViewModel), nameof(ValidateBypass))]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _userName;

        //public static ValidationResult? ValidateEmail(string userName, ValidationContext context)
        //{
        //    if (userName.Length < 1)
        //    {
        //        return new("Please enter an email address.");
        //    }

        //    return ValidationResult.Success;
        //}

        //[Required]
        //[EmailAddress]
        //[CustomValidation(typeof(LoginViewModel), nameof(ValidateBypass))]
        //[NotifyDataErrorInfo]
        //[ObservableProperty]
        //private string _emailRegister;

        //[Required]
        //[PasswordPropertyText]
        //[CustomValidation(typeof(LoginViewModel), nameof(ValidateBypass))]
        //[NotifyDataErrorInfo]
        //[ObservableProperty]
        //private string _passwordRegister;

        [Required]
        [PasswordPropertyText]
        //[CustomValidation(typeof(LoginViewModel), nameof(ValidateBypass))]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _passwordLogin;

        //[Required]
        //[MinLength(1)]
        //[MaxLength(50)]
        //[CustomValidation(typeof(LoginViewModel), nameof(ValidateBypass))]
        //[NotifyDataErrorInfo]
        //[ObservableProperty]
        //private string _firstName;

        //[Required]
        //[MinLength(1)]
        //[MaxLength(50)]
        //[CustomValidation(typeof(LoginViewModel), nameof(ValidateBypass))]
        //[NotifyDataErrorInfo]
        //[ObservableProperty]
        //private string _lastName;

        [ObservableProperty]
        private bool _toggleRegister = false;

        [ObservableProperty]
        private bool _toggleLogin = true;
        
        //public ObservableCollection<ValidationResult> RegisterErrors { get; } = new();

        public ObservableCollection<ValidationResult> LoginErrors { get; } = new();


        public static ValidationResult? ValidateBypass(string? input, ValidationContext context)
        {
            if (input is "hidden" or "hid@den")
            {
                return ValidationResult.Success; // Return success if the field is null or empty
            }
            //if (string.IsNullOrWhiteSpace(input))
            //{

            //    return new ValidationResult("All fields required");
            //}
            //else if (!IsValidEmail(email)) // Custom email validation logic
            //{
            //    return new ValidationResult("Please enter a valid email address.");
            //}

            return ValidationResult.Success;
        }

        //private void ToggleLoginFields()
        //{
        //        EmailRegister = "hid@den";
        //        PasswordRegister = "hidden";
        //        FirstName = "hidden";
        //        LastName = "hidden";
        //        //UserName = "";
        //        //PasswordLogin = "";
        //}

        private void ToggleRegisterFields()
        {
                //EmailRegister = "";
                //PasswordRegister = "";
                //FirstName = "";
                //LastName = "";
                UserName = "hid@den";
                PasswordLogin = "hidden";
        }

        public LoginViewModel(ClientService clientService, IUserService userService, INavigationService navigationService, IDialogService dialogService)
        {
            _clientService = clientService;
            _userService = userService;
            _navigationService = navigationService;
            _dialogService = dialogService;
            //RegisterModel = new();
            //LoginModel = new();
            IsAuthenticated = false;
            failedAttempt = false;
            loginAttempts = 0;
            //GetUserNameFromSecuredStorage();

            WeakReferenceMessenger.Default.Register<LogoutMessage>(this);
            WeakReferenceMessenger.Default.Register<LoginMessage>(this);

            //ErrorsChanged += RegisterUserViewModel_ErrorsChanged;
            ErrorsChanged += LoginUserViewModel_ErrorsChanged;
        }

        //public void BypassHiddenFields()
        //{
        //    if (ToggleLogin == true)
        //    {
        //        ToggleLoginFields();
        //    }
        //    else
        //    {
        //        ToggleRegisterFields();
        //    }
        //}

        [RelayCommand]
        private async Task SignUpLabel()
        {
            await _navigationService.GoToRegister();

            //(ToggleRegister, ToggleLogin) = (ToggleLogin, ToggleRegister);
            //BypassHiddenFields();
            //RegisterErrors.Clear();
            //LoginErrors.Clear();

            //ErrorsChanged -= LoginUserViewModel_ErrorsChanged;

            //if (ToggleLogin == true)
            //{
            //    UserName = default!;
            //    PasswordLogin = default!;
            //    //ErrorsChanged -= RegisterUserViewModel_ErrorsChanged;
            //}
            //else
            //{
            //    EmailRegister = default!;
            //    PasswordRegister = default!;
            //    FirstName = default!;
            //    LastName = default!;
            //    LoginErrors.Clear();
            //    RegisterErrors.Clear();
            //    //ErrorsChanged -= LoginUserViewModel_ErrorsChanged;
            //}
        }

        //[RelayCommand(CanExecute = nameof(CanRegisterUser))]
        //private async Task Register()
        //{
        //    ToggleRegisterFields();
        //    ValidateAllProperties();
        //    if (RegisterErrors.Any())
        //    {
        //        return;
        //    }

        //    RegisterModel registerModel = MapDataToRegisterModel();
            
        //    await _clientService.Register(registerModel);

        //    Guid userId = await _userService.GetGuid(EmailRegister);

        //    UserModel? newUser = await _userService.GetUser(userId);

        //    if (newUser != null)
        //    {
        //        if (FirstName == null && LastName == null)
        //        {
        //            newUser.FirstName = EmailRegister;
        //        }
        //        newUser.FirstName = FirstName!;
        //        newUser.LastName = LastName!;
        //        if (await _userService.PutUser(newUser))
        //        {
        //            WeakReferenceMessenger.Default.Send(new UserCreatedMessage());
        //            await _dialogService.Notify("Success", "You have been registered.");
        //            await _navigationService.GoToUserDetail(userId);
        //        }
        //        else
        //        {
        //            await _dialogService.Notify("Failed", "User Registration failed.");
        //        }
        //    }
        //    else
        //    {
        //        await _dialogService.Notify("Failed", "User Registration failed.");
        //    }
        //}

        ////private bool CanRegisterUser() => !HasErrors;
        //private bool CanRegisterUser() => ToggleRegister && !HasErrors;

        [RelayCommand(CanExecute = nameof(CanLoginUser))]
        private async Task Login()
        {
            //ToggleLoginFields();
            failedAttempt = false;
            ValidateAllProperties();
            if (LoginErrors.Any())
            {
                return;
            }

            LockoutInfoModel lockoutEnd = await _userService.GetLockoutEnd(UserName);

            LoginModel loginModel = MapDataToLoginModel();

            //LoginModel.Email = Email;
            //LoginModel.Password = Password;
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
                //WeakReferenceMessenger.Default.Send(new LoginMessage());
                //await _dialogService.Notify("Success", "You are logged in.");
                await _navigationService.GoToUserDetail(userId);
            }
            else
            {
                await _dialogService.Notify("Failed", "Login Failed.");
            }
        }

        //private bool CanLoginUser() => !HasErrors;
        private bool CanLoginUser() => !HasErrors;


        //[RelayCommand]
        private async Task Logout()
        {
            //await _dialogService.Notify("Success", "You have been logged out.");
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            var serializedLogoutResponseInStorage = await SecureStorage.Default.GetAsync("RegPW");
            string tempUserName = JsonSerializer.Deserialize<LoginResponseModel>(serializedLoginResponseInStorage)!.UserName!;
            if (tempUserName == UserName && serializedLogoutResponseInStorage != PasswordLogin)
            {
                
            }
            //else if (serializedLoginResponseInStorage != null && serializedLogoutResponseInStorage != null)
            else
            {
                UserName = tempUserName;
                PasswordLogin = JsonSerializer.Deserialize<string>(serializedLogoutResponseInStorage)!;
            }
            
            SecureStorage.Default.Remove("Authentication");
            IsAuthenticated = false;
        }
        
        //private RegisterModel MapDataToRegisterModel()
        //{
        //    return new RegisterModel
        //    {
        //        Email = EmailRegister,
        //        Password = PasswordRegister
        //    };
        //}

        private LoginModel MapDataToLoginModel()
        {
            return new LoginModel
            {
                Email = UserName,
                Password = PasswordLogin
            };
        }

        //public override async Task LoadAsync()
        //{
        //    await Loading(
        //        async () =>
        //        {
        //            if (IsAuthenticated == true)
        //            {
        //                await Logout();
        //            }
        //        });
        //}

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

        //private void RegisterUserViewModel_ErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
        //{
        //    RegisterErrors.Clear();
        //    GetErrors().ToList().ForEach(RegisterErrors.Add);
        //    RegisterCommand.NotifyCanExecuteChanged();
        //}

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
            //message.LoadFromXaml(UserName);
            //ToggleRegister = false;
            //ToggleLogin = true;
            //IsAuthenticated = false;
        }

        public void Receive(LoginMessage message)
        {
            failedAttempt = true;
            loginAttempts++;
        }

            //[RelayCommand]
            //private async Task GoToWeatherForecast()
            //{
            //    await Shell.Current.GoToAsync(nameof(WeatherForecastPage));
            //}
        }
}
