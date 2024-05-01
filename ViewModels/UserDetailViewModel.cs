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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.ViewModels
{
    public partial class UserDetailViewModel : ViewModelBase, IQueryAttributable
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        [ObservableProperty]
        private Guid _id;
        [ObservableProperty]
        private string _firstName;
        [ObservableProperty]
        private string _lastName;
        [ObservableProperty]
        private string _fullName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(HideBalanceCommand))]
        public decimal? _balance;

        [ObservableProperty]
        public bool _checkSummary;

        [ObservableProperty]
        private DateTime? _date = DateTime.Now;

        [ObservableProperty]
        private string _balanceColor;

        [RelayCommand]
        private void HideBalance() => Balance = null;

        [RelayCommand]
        private async Task ViewIncomes()
        {
            await _navigationService.GoToUserIncomes(Id);
        }

        [RelayCommand]
        private async Task ViewExpenses()
        {
            await _navigationService.GoToUserExpenses(Id);
        }

        [RelayCommand]
        private async Task ViewTransactions()
        {
            await _navigationService.GoToTransactions(Id);
        }

        [RelayCommand]
        private async Task ViewGoals()
        {
            await _navigationService.GoToGoals(Id);
        }

        [RelayCommand]
        private async Task NavigateToAddIncome()
            => await _navigationService.GoToAddIncome(Id);

        [RelayCommand]
        private async Task NavigateToAddExpense()
            => await _navigationService.GoToAddExpense(Id);

        public UserDetailViewModel(IUserService userService, INavigationService navigationService, IDialogService dialogService) 
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
                    await GetUser(Id);
                    await CheckFinancialSummary(Id);
                    if (CheckSummary == true)
                    {
                        await GetCurrentBalance(Id);
                        if (Balance > 0)
                        {
                            BalanceColor = "Green";
                        }
                        else if (Balance < 0)
                        {
                            BalanceColor = "Red";
                        }
                        else
                        {
                            BalanceColor = "Black";
                        }
                    }
                    else
                    {
                        Balance = 0;
                    }
                });
        }

        private async Task GetUser(Guid id)
        {
            var user = await _userService.GetUser(id);

            if (user != null)
            {
                MapUserData(user);
            }
        }

        private async Task CheckFinancialSummary(Guid id)
        {
            bool checkSummary = await _userService.CheckFinancialSummary(id);

            MapUserSummary(checkSummary);
        }

        private async Task GetCurrentBalance(Guid id)
        {
            var balance = await _userService.GetCurrentBalance(id);

            if (balance != null)
            {
                MapUserBalance((decimal)balance);
            }
        }

        private void MapUserData(UserModel user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            FullName = FirstName + " " + LastName;
        }

        private void MapUserBalance(decimal balance)
        {
            Balance = balance;
        }

        private void MapUserSummary(bool checkSummary)
        {
            CheckSummary = checkSummary;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.Count > 0)
            {
                Guid userId = (Guid) query["UserId"];

                Id = userId;
            }
        }
    }
}
