// Clayton DeSimone
// .NET Applications
// Final Project
// 4/29/2024

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
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.ViewModels
{
    public partial class UserExpensesListOverviewViewModel : ViewModelBase, IQueryAttributable, IRecipient<ExpenseAddedOrChangedMessage>
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        [ObservableProperty]
        private Guid _userId;

        [ObservableProperty]
        private DateTime _startDate = DateTime.Now.AddMonths(-1);

        [ObservableProperty]
        private DateTime _endDate = DateTime.Now;

        [ObservableProperty]
        private DateTime _minDate = DateTime.Now.AddYears(-4);

        [ObservableProperty]
        private DateTime _maxDate = DateTime.Today;

        [ObservableProperty]
        private ObservableCollection<UserExpensesListItemViewModel> _expenses = new();

        public List<ExpenseModel> AllExpenses = new();

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

        private bool viewAllClicked = false;

        [RelayCommand]
        private async Task ViewAllExpenses()
        {
            await GetExpenses(UserId);
            if (Expenses.Count != 0)
            {
                viewAllClicked = true;
                StartDate = Expenses.LastOrDefault()!.DateIncurred;
                EndDate = Expenses.FirstOrDefault()!.DateIncurred;
                viewAllClicked = false;
            }
            else
            {
                await _dialogService.Notify("Empty", "No expenses have been added.");
            }
        }

        [RelayCommand]
        private async Task Back()
        {
            await _navigationService.GoToUserDetail(UserId);
        }

        public UserExpensesListOverviewViewModel(IUserService userService, INavigationService navigationService, IDialogService dialogService)
        {
            _userService = userService;
            _navigationService = navigationService;
            _dialogService = dialogService;

            WeakReferenceMessenger.Default.Register(this);

            PropertyChanged += OnPropertyChanged!;
        }

        private async void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(StartDate) || e.PropertyName == nameof(EndDate))
            {
                await ReloadExpenses();
            }
        }

        [RelayCommand]
        private async Task ReloadExpenses()
        {
            if (viewAllClicked == false)
            {
                await GetExpensesForDateRange(UserId, StartDate, EndDate);
            }
        }

        public override async Task LoadAsync()
        {
            await Loading(
                async () =>
                {
                    AllExpenses = await _userService.GetExpenses(UserId);
                    if (AllExpenses.Any())
                    {
                        MinDate = AllExpenses.FirstOrDefault()!.DateIncurred;
                    }
                    await GetExpensesForDateRange(UserId, StartDate, EndDate);
                });
        }

        private async Task GetExpensesForDateRange(Guid userId, DateTime startDate, DateTime endDate)
        {
            List<ExpenseModel> expenses = await _userService.GetExpensesForDateRange(userId, startDate, endDate);
            List<UserExpensesListItemViewModel> listItems = new();
            foreach (var expense in expenses)
            {
                listItems.Insert(0, MapExpenseModelToUserExpensesListItemViewModel(expense));
            }

            Expenses.Clear();
            Expenses = listItems.ToObservableCollection();
        }

        private async Task GetExpenses(Guid id)
        {
            List<UserExpensesListItemViewModel> listItems = new();
            foreach (var expense in AllExpenses)
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
            }
        }

        public async void Receive(ExpenseAddedOrChangedMessage message)
        {
            Expenses.Clear();
            await GetExpensesForDateRange(UserId, StartDate, EndDate);
        }
    }
}
