// Clayton DeSimone
// .NET Applications
// Final Project
// 4/29/2024

using CommunityToolkit.Mvvm.ComponentModel;
using FinanceMAUI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using FinanceMAUI.Services;
using FinanceMAUI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FinanceMAUI.Messages;
using CommunityToolkit.Mvvm.Input;

namespace FinanceMAUI.ViewModels
{
    public partial class IncomeAddEditViewModel : ViewModelBase, IQueryAttributable
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        public IncomeModel? incomeDetail;

        [ObservableProperty]
        private string _incomePageTitle;

        [ObservableProperty]
        private int _incomeId;

        [MinLength(1)]
        [MaxLength(50)]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string? _source;
        
        [Required]
        [CustomValidation(typeof(IncomeAddEditViewModel), nameof(ValidateAmount))]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private decimal _amount;

        public static ValidationResult? ValidateAmount(decimal amount, ValidationContext context)
        {
            if (amount < (decimal) 0.01 || amount > 999999999)
            {
                return new("Amount must be above 0 and below 1 billion.");
            }

            return ValidationResult.Success;
        }
        
        [Required]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private DateTime _dateReceived = DateTime.Now;

        [ObservableProperty]
        private DateTime _minDate = DateTime.Now.AddYears(-4);

        [ObservableProperty]
        private DateTime _maxDate = DateTime.Now;

        [ObservableProperty]
        private Guid _userId;

        public ObservableCollection<ValidationResult> Errors { get; } = new();

        [RelayCommand]
        private async Task Back()
        {
            if (IncomeId > 0)
            {
                await _navigationService.GoToUserIncomes(UserId);
            }
            else
            {
                await _navigationService.GoToUserDetail(UserId);
            }
        }

        [RelayCommand(CanExecute = nameof(CanSubmitIncome))]
        private async Task Submit()
        {
            ValidateAllProperties();
            if (Errors.Any())
            {
                return;
            }

            IncomeModel model = MapDataToIncomeModel();

            if (incomeDetail == null)
            {
                if (await _userService.CreateIncome(model))
                {
                    WeakReferenceMessenger.Default.Send(new IncomeAddedOrChangedMessage());
                    await _dialogService.Notify("Success", "The income is added.");
                    await _navigationService.GoToUserDetail(UserId);
                }
                else
                {
                    await _dialogService.Notify("Failed", "Source field is empty.");
                }
            }
            else
            {
                if (await _userService.EditIncome(model))
                {
                    WeakReferenceMessenger.Default.Send(new IncomeAddedOrChangedMessage());
                    await _dialogService.Notify("Success", "The income is updated.");
                    await _navigationService.GoToUserIncomes(UserId);
                }
                else
                {
                    await _dialogService.Notify("Failed", "Editing the income failed.");
                }
            }
        }

        private bool CanSubmitIncome() => !HasErrors;



        public IncomeAddEditViewModel(IUserService userService, INavigationService navigationService, IDialogService dialogService)
        {
            _userService = userService;
            _navigationService = navigationService;
            _dialogService = dialogService;

            _amount = 0.01m;

            ErrorsChanged += AddIncomeViewModel_ErrorsChanged;
        }

        public override async Task LoadAsync()
        {
            await Loading(
                async () =>
                {
                    if (incomeDetail is null && IncomeId > 0)
                    {
                        incomeDetail = await _userService.GetIncome(UserId, IncomeId);
                    }
                    MapIncome(incomeDetail);

                    ValidateAllProperties();
                });
        }

        private void AddIncomeViewModel_ErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            Errors.Clear();
            GetErrors().ToList().ForEach(Errors.Add);
            SubmitCommand.NotifyCanExecuteChanged();
        }

        private void MapIncome(IncomeModel? model)
        {
            if (model is not null)
            {
                IncomeId = model.IncomeId;
                Source = model.Source;
                Amount = model.Amount;
                DateReceived = model.DateReceived;
                UserId = model.Id;
            }

            IncomePageTitle = IncomeId > 0 ? "Edit Income" : "Add Income";
        }

        private IncomeModel MapDataToIncomeModel()
        {
            return new IncomeModel
            {
                IncomeId = IncomeId,
                Source = Source ?? string.Empty,
                Amount = Amount,
                DateReceived = DateReceived,
                Id = UserId
            };
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.Count > 0)
            {
                if (query.ContainsKey("UserId"))
                {
                    Guid userId = (Guid)query["UserId"];

                    UserId = userId;
                }
                else
                {
                    incomeDetail = query["Income"] as IncomeModel;
                }
            }
        }
    }
}
