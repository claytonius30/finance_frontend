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
using System.Threading.Tasks;

namespace FinanceMAUI.ViewModels
{
    public partial class ExpenseAddEditViewModel : ViewModelBase, IQueryAttributable
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        public ExpenseModel? expenseDetail;

        [ObservableProperty]
        private string _expensePageTitle;

        [ObservableProperty]
        private int _expenseId;

        [MinLength(1)]
        [MaxLength(50)]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private string? _category;

        [Required]
        [CustomValidation(typeof(ExpenseAddEditViewModel), nameof(ValidateAmount))]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private decimal _amount;

        public static ValidationResult? ValidateAmount(decimal amount, ValidationContext context)
        {
            if (amount < (decimal)0.01 || amount > 999999999)
            {
                return new("Amount must be above 0 and below 1 billion.");
            }

            return ValidationResult.Success;
        }

        [Required]
        [NotifyDataErrorInfo]
        [ObservableProperty]
        private DateTime _dateIncurred = DateTime.Now;

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
            if (ExpenseId > 0)
            {
                await _navigationService.GoToUserExpenses(UserId);
            }
            else
            {
                await _navigationService.GoToUserDetail(UserId);
            }
        }

        [RelayCommand(CanExecute = nameof(CanSubmitExpense))]
        private async Task Submit()
        {
            ValidateAllProperties();
            if (Errors.Any())
            {
                return;
            }

            ExpenseModel model = MapDataToExpenseModel();

            if (expenseDetail == null)
            {
                if (await _userService.CreateExpense(model))
                {
                    WeakReferenceMessenger.Default.Send(new ExpenseAddedOrChangedMessage());
                    await _dialogService.Notify("Success", "The expense is added.");
                    await _navigationService.GoToUserDetail(UserId);
                }
                else
                {
                    await _dialogService.Notify("Failed", "Category field is empty.");
                }
            }
            else
            {
                if (await _userService.EditExpense(model))
                {
                    WeakReferenceMessenger.Default.Send(new ExpenseAddedOrChangedMessage());
                    await _dialogService.Notify("Success", "The expense is updated.");
                    await _navigationService.GoToUserExpenses(UserId);
                }
                else
                {
                    await _dialogService.Notify("Failed", "Editing the expense failed.");
                }
            }
        }

        private bool CanSubmitExpense() => !HasErrors;

        public ExpenseAddEditViewModel(IUserService userService, INavigationService navigationService, IDialogService dialogService)
        {
            _userService = userService;
            _navigationService = navigationService;
            _dialogService = dialogService;

            _amount = 0.01m;

            ErrorsChanged += AddExpenseViewModel_ErrorsChanged;
        }

        public override async Task LoadAsync()
        {
            await Loading(
                async () =>
                {
                    if (expenseDetail is null && ExpenseId > 0)
                    {
                        expenseDetail = await _userService.GetExpense(UserId, ExpenseId);
                    }
                    MapExpense(expenseDetail);

                    ValidateAllProperties();
                });
        }

        private void AddExpenseViewModel_ErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            Errors.Clear();
            GetErrors().ToList().ForEach(Errors.Add);
            SubmitCommand.NotifyCanExecuteChanged();
        }

        private void MapExpense(ExpenseModel? model)
        {
            if (model is not null)
            {
                ExpenseId = model.ExpenseId;
                Category = model.Category;
                Amount = model.Amount;
                DateIncurred = model.DateIncurred;
                UserId = model.Id;
            }

            ExpensePageTitle = ExpenseId > 0 ? "Edit Expense" : "Add Expense";
        }

        private ExpenseModel MapDataToExpenseModel()
        {
            return new ExpenseModel
            {
                ExpenseId = ExpenseId,
                Category = Category ?? string.Empty,
                Amount = Amount,
                DateIncurred = DateIncurred,
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
                    expenseDetail = query["Expense"] as ExpenseModel;
                }
            }
        }
    }
}
