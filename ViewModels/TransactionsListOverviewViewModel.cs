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
    public partial class TransactionsListOverviewViewModel : ViewModelBase, IQueryAttributable
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private Guid _userId;

        [ObservableProperty]
        private ObservableCollection<TransactionsListItemViewModel> _transactions = new();
        [ObservableProperty]
        private TransactionsListItemViewModel? _selectedTransaction;

        //[RelayCommand]
        //private async Task NavigateToSelectedDetail()
        //{
        //    if (SelectedIncome is not null)
        //    {
        //        await _navigationService.GoToIncomeDetail(UserId, SelectedIncome.IncomeId);
        //        SelectedIncome = null;
        //    }
        //}

        [RelayCommand]
        private async Task Back()
        {
            await _navigationService.GoToUserDetail(UserId);
        }

        public TransactionsListOverviewViewModel(IUserService userService, INavigationService navigationService)
        {
            _userService = userService;
            _navigationService = navigationService;

            //WeakReferenceMessenger.Default.Register(this);
        }

        public override async Task LoadAsync()
        {
            await Loading(
                async () =>
                {
                    await GetAllTransactions(UserId);
                });
        }

        private async Task GetAllTransactions(Guid id)
        {
            List<TransactionModel> transactions = await _userService.GetAllTransactions(id);
            List<TransactionsListItemViewModel> listItems = new();
            foreach (var transaction in transactions)
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

        //public async void Receive(IncomeAddedOrChangedMessage message)
        //{
        //    Incomes.Clear();
        //    await GetIncomes(UserId);
        //}
    }
}
