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
using System.Transactions;

namespace FinanceMAUI.ViewModels
{
    public partial class TransactionsListOverviewViewModel : ViewModelBase, IQueryAttributable
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        [ObservableProperty]
        private Guid _userId;

        [ObservableProperty]
        public decimal? _balanceForDateRange;

        [ObservableProperty]
        public bool _isElementVisible = false;

        [ObservableProperty]
        private string _balanceColor;

        [ObservableProperty]
        private DateTime _startDate = DateTime.Now.AddMonths(-1);

        [ObservableProperty]
        private DateTime _endDate = DateTime.Now;

        [ObservableProperty]
        private DateTime _minDate;

        [ObservableProperty]
        private DateTime _maxDate = DateTime.Today;

        [ObservableProperty]
        private ObservableCollection<TransactionsListItemViewModel> _transactions = new();

        public List<TransactionModel> AllTransactions = new();

        private bool viewAllClicked = false;

        [RelayCommand]
        private async Task ViewAllTransactions()
        {
            GetAllTransactions(UserId);
            if (Transactions.Count != 0)
            {
                viewAllClicked = true;
                StartDate = AllTransactions.FirstOrDefault()!.Date;
                EndDate = AllTransactions.LastOrDefault()!.Date;
                viewAllClicked = false;
            }
            else
            {
                await _dialogService.Notify("Empty", "No transactions have been added.");
            }
        }

        [RelayCommand]
        private async Task Back()
        {
            await _navigationService.GoToUserDetail(UserId);
        }

        public TransactionsListOverviewViewModel(IUserService userService, INavigationService navigationService, IDialogService dialogService)
        {
            _userService = userService;
            _navigationService = navigationService;
            _dialogService = dialogService;

            PropertyChanged += OnPropertyChanged!;
        }

        // Used to keep Range Balance hidden when page first loads
        private int count = 0;

        private async void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (count > 7)
            {
                IsElementVisible = true;
                if (viewAllClicked == false)
                {
                    if (e.PropertyName == nameof(StartDate) || e.PropertyName == nameof(EndDate))
                    {
                        BalanceForDateRange = await _userService.GetBalanceForDateRange(UserId, StartDate, EndDate);
                        
                        await ReloadTransactions();
                    }
                }
                else
                {
                    BalanceForDateRange = await _userService.GetCurrentBalance(UserId);
                }
                if (e.PropertyName == nameof(BalanceForDateRange))
                {
                    if (BalanceForDateRange > 0)
                    {
                        BalanceColor = "Green";
                    }
                    else if (BalanceForDateRange < 0)
                    {
                        BalanceColor = "Red";
                    }
                    else
                    {
                        BalanceColor = "Black";
                    }
                }
            }
            count++;
        }

        [RelayCommand]
        private async Task ReloadTransactions()
        {
            await GetTransactionsForDateRange(UserId, StartDate, EndDate);
        }

        public override async Task LoadAsync()
        {
            await Loading(
            async () =>
            {
                AllTransactions = await _userService.GetAllTransactions(UserId);
                if (AllTransactions.Any())
                {
                    MinDate = AllTransactions.FirstOrDefault()!.Date;
                    await GetTransactionsForDateRange(UserId, StartDate, EndDate);
                }
            });
        }

        private async Task GetTransactionsForDateRange(Guid userId, DateTime startDate, DateTime endDate)
        {
            List<TransactionModel> transactions = await _userService.GetTransactionsForDateRange(userId, startDate, endDate);
            List<TransactionsListItemViewModel> listItems = new();
            foreach (var transaction in transactions)
            {
                listItems.Insert(0, MapTransactionModelToTransactionsListItemViewModel(transaction));
            }
            Transactions.Clear();
            Transactions = listItems.ToObservableCollection();
        }

        private void GetAllTransactions(Guid userId)
        {
            List<TransactionsListItemViewModel> listItems = new();
            foreach (var transaction in AllTransactions)
            {
                listItems.Insert(0, MapTransactionModelToTransactionsListItemViewModel(transaction));
            }
            Transactions.Clear();
            Transactions = listItems.ToObservableCollection();
        }

        private TransactionsListItemViewModel MapTransactionModelToTransactionsListItemViewModel(TransactionModel transaction)
        {
            return new TransactionsListItemViewModel(
                transaction.Type,
                transaction.Id,
                transaction.Amount,
                transaction.Description,
                transaction.Date);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.Count > 0)
            {
                Guid userId = (Guid)query["UserId"];

                UserId = userId;
            }
        }
    }
}
