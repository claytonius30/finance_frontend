using CommunityToolkit.Maui.Core.Extensions;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.ViewModels
{
    public partial class UserExpensesListOverviewViewModel : ViewModelBase, IQueryAttributable, IRecipient<ExpenseAddedOrChangedMessage>
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private Guid _userId;

        [ObservableProperty]
        private ObservableCollection<UserExpensesListItemViewModel> _expenses = new();
        [ObservableProperty]
        private UserExpensesListItemViewModel? _selectedExpense;

        [RelayCommand]
        private async Task NavigateToSelectedDetail()
        {
            if (SelectedExpense is not null)
            {
                await _navigationService.GoToExpenseDetail(UserId, SelectedExpense.ExpenseId);
                SelectedExpense = null;
            }
        }

        [RelayCommand]
        private async Task Back()
        {
            await _navigationService.GoToUserDetail(UserId);
        }

        public UserExpensesListOverviewViewModel(IUserService userService, INavigationService navigationService)
        {
            _userService = userService;
            _navigationService = navigationService;

            WeakReferenceMessenger.Default.Register(this);

            //Id = 1;
            //GetIncomes(Id);

            //Incomes = new List<UserIncomesListItemViewModel>
            //{
            //    new(1,
            //        "salary",
            //        (decimal) 600.55,
            //        DateTime.Now),
            //    new(2,
            //        "gigs",
            //        (decimal) 205.98,
            //        DateTime.Now.AddDays(-3))
            //};
        }

        public override async Task LoadAsync()
        {
            await Loading(
                async () =>
                {
                    await GetExpenses(UserId);
                });
        }

        private async Task GetExpenses(Guid id)
        {
            List<ExpenseModel> expenses = await _userService.GetExpenses(id);
            List<UserExpensesListItemViewModel> listItems = new();
            foreach (var expense in expenses)
            {
                listItems.Insert(0, MapExpenseModelToUserExpensesListItemViewModel(expense));
            }

            Expenses.Clear();
            Expenses = listItems.ToObservableCollection();
        }

        private UserExpensesListItemViewModel MapExpenseModelToUserExpensesListItemViewModel(ExpenseModel expense)
        {
            return new UserExpensesListItemViewModel(
                expense.ExpenseId,
                expense.Category,
                expense.Amount,
                expense.DateIncurred,
                expense.Id);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.Count > 0)
            {
                Guid userId = (Guid)query["UserId"];

                UserId = userId;
                //await GetExpenses(Id);
            }
        }

        public async void Receive(ExpenseAddedOrChangedMessage message)
        {
            Expenses.Clear();
            await GetExpenses(UserId);
        }
    }
}
