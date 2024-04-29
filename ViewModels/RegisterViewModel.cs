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
    public partial class RegisterViewModel : ViewModelBase, IRecipient<RegisterMessage>
    {
        private readonly IClientService _clientService;
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        [ObservableProperty]
        private bool _isAuthenticated;
        private bool emailExists;

        [Required]
        [EmailAddress]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _emailRegister;
        
        [Required]
        [PasswordPropertyText]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _passwordRegister;

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _firstName;

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string _lastName;

        public ObservableCollection<ValidationResult> RegisterErrors { get; } = new();

        public RegisterViewModel(IClientService clientService, IUserService userService, INavigationService navigationService, IDialogService dialogService)
        {
            _clientService = clientService;
            _userService = userService;
            _navigationService = navigationService;
            _dialogService = dialogService;
            IsAuthenticated = false;
            emailExists = false;

            WeakReferenceMessenger.Default.Register<RegisterMessage>(this);

            ErrorsChanged += RegisterUserViewModel_ErrorsChanged;
        }


        [RelayCommand]
        private async Task LoginLabel()
        {
            await _navigationService.GoToOverview();
        }

        [RelayCommand(CanExecute = nameof(CanRegisterUser))]
        private async Task Register()
        {
            emailExists = false;
            ValidateAllProperties();
            if (RegisterErrors.Any())
            {
                return;
            }

            RegisterModel registerModel = MapDataToRegisterModel();

            await _clientService.Register(registerModel);
            if (emailExists == false)
            {
                GetEmailFromSecuredStorage();
            }
            else
            {
                await _dialogService.Notify("Failed", "Email already exists.");
                return;
            }
            if (IsAuthenticated)
            {
                Guid userId = await _userService.GetGuid(EmailRegister);
                UserModel? newUser = await _userService.GetUser(userId);

                if (newUser != null)
                {
                    newUser.FirstName = FirstName!;
                    newUser.LastName = LastName!;
                    if (await _userService.PutUser(newUser))
                    {
                        await _dialogService.Notify("Success", "You have been registered.");
                        await _navigationService.GoToUserDetail(userId);
                    }
                    else
                    {
                        await _dialogService.Notify("Failed", "User Registration failed.");
                    }
                }
            }
            else
            {
                await _dialogService.Notify("Failed", "User Registration failed.");
            }
        }
        
        private bool CanRegisterUser() => !HasErrors;

        private RegisterModel MapDataToRegisterModel()
        {
            return new RegisterModel
            {
                Email = EmailRegister,
                Password = PasswordRegister
            };
        }

        private async void GetEmailFromSecuredStorage()
        {
            if (!string.IsNullOrEmpty(EmailRegister) && emailExists == false)
            {
                IsAuthenticated = true;
                return;
            }
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            if (serializedLoginResponseInStorage != null && emailExists == false)
            {
                EmailRegister = JsonSerializer.Deserialize<LoginResponseModel>(serializedLoginResponseInStorage)!.UserName!;
                IsAuthenticated = true;
                return;
            }
            IsAuthenticated = false;
        }

        private void RegisterUserViewModel_ErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            RegisterErrors.Clear();
            GetErrors().ToList().ForEach(RegisterErrors.Add);
            RegisterCommand.NotifyCanExecuteChanged();
        }

        public void Receive(RegisterMessage message)
        {
            emailExists = true;
        }
    }
}
