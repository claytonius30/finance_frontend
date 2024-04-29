using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinanceMAUI.Models;
using FinanceMAUI.Services;
using FinanceMAUI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.ViewModels
{
    public partial class ExpenseDetailViewModel : ViewModelBase, IQueryAttributable
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        [ObservableProperty]
        private int _expenseId;
        [ObservableProperty]
        private string _category;
        [ObservableProperty]
        private decimal _amount;
        [ObservableProperty]
        private DateTime _dateIncurred;
        [ObservableProperty]
        private Guid _userId;

        [RelayCommand]
        private async Task NavigateToEditExpense()
        {
            var detailModel = MapToExpenseModel(this);
            await _navigationService.GoToEditExpense(detailModel);
        }

        [RelayCommand]
        public async Task DeleteExpense()
        {
            if (await _dialogService.Ask(
                    "Delete expense",
                    "Are you sure you want to delete this expense?"))
            {
                if (await _userService.DeleteExpense(UserId, ExpenseId))
                {
                    await _navigationService.GoToUserExpenses(UserId);
                    await _dialogService.Notify("Success", $"The expense '{Category}' is deleted.");
                }
            }
        }

        [RelayCommand]
        private async Task Back()
        {
            await _navigationService.GoToUserExpenses(UserId);
        }

        public ExpenseDetailViewModel(IUserService userService, INavigationService navigationService, IDialogService dialogService)
        {
            _userService = userService;
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        public override async Task LoadAsync()
        {
            await Loading(
                async () =>
                {
                    await GetExpense(UserId, ExpenseId);
                });
        }

        private async Task GetExpense(Guid userId, int expenseId)
        {
            var expense = await _userService.GetExpense(userId, expenseId);

            if (expense != null)
            {
                MapExpenseData(expense);
            }
        }

        private void MapExpenseData(ExpenseModel expense)
        {
            ExpenseId = expense.ExpenseId;
            Category = expense.Category;
            Amount = expense.Amount;
            DateIncurred = expense.DateIncurred;
            UserId = expense.Id;
        }

        private ExpenseModel MapToExpenseModel(ExpenseDetailViewModel expenseViewModel)
        {
            return new ExpenseModel
            {
                ExpenseId = expenseViewModel.ExpenseId,
                Category = expenseViewModel.Category,
                Amount = expenseViewModel.Amount,
                DateIncurred = expenseViewModel.DateIncurred,
                Id = expenseViewModel.UserId
            };
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Guid userId = (Guid)query["UserId"];
            int expenseId = (int)query["ExpenseId"];

            UserId = userId;
            ExpenseId = expenseId;
        }
    }
}
