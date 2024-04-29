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
    public partial class UserIncomesListOverviewViewModel : ViewModelBase, IQueryAttributable, IRecipient<IncomeAddedOrChangedMessage>
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
        private ObservableCollection<UserIncomesListItemViewModel> _incomes = new();
        [ObservableProperty]
        private UserIncomesListItemViewModel? _selectedIncome;

        [RelayCommand]
        private async Task NavigateToSelectedDetail()
        {
            if (SelectedIncome is not null)
            {
                await _navigationService.GoToIncomeDetail(UserId, SelectedIncome.IncomeId);
                SelectedIncome = null;
            }
        }

        [RelayCommand]
        private async Task ViewAllIncomes()
        {
            await GetIncomes(UserId);
            if (Incomes.Count != 0)
            {
                StartDate = Incomes.LastOrDefault()!.DateReceived;
                EndDate = Incomes.FirstOrDefault()!.DateReceived;
            }
            else
            {
                await _dialogService.Notify("Empty", "No incomes have been added.");
            }
        }

        [RelayCommand]
        private async Task Back()
        {
            await _navigationService.GoToUserDetail(UserId);
        }

        public UserIncomesListOverviewViewModel(IUserService userService, INavigationService navigationService, IDialogService dialogService)
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
                await ReloadIncomes();
            }
        }

        [RelayCommand]
        private async Task ReloadIncomes()
        {
            await GetIncomesForDateRange(UserId, StartDate, EndDate);
        }

        public override async Task LoadAsync()
        {
            await Loading(
                async () =>
                {
                    await GetIncomesForDateRange(UserId, StartDate, EndDate);
                });
        }

        private async Task GetIncomesForDateRange(Guid userId, DateTime startDate, DateTime endDate)
        {
            List<IncomeModel> incomes = await _userService.GetIncomesForDateRange(userId, startDate, endDate.AddDays(1));
            List<UserIncomesListItemViewModel> listItems = new();
            foreach (var income in incomes)
            {
                listItems.Insert(0, MapIncomeModelToUserIncomesListItemViewModel(income));
            }

            Incomes.Clear();
            Incomes = listItems.ToObservableCollection();
        }

        private async Task GetIncomes(Guid id)
        {
            List<IncomeModel> incomes = await _userService.GetIncomes(id);
            List<UserIncomesListItemViewModel> listItems = new();
            foreach (var income in incomes)
            {
                listItems.Insert(0, MapIncomeModelToUserIncomesListItemViewModel(income));
            }

            Incomes.Clear();
            Incomes = listItems.ToObservableCollection();
        }

        private UserIncomesListItemViewModel MapIncomeModelToUserIncomesListItemViewModel(IncomeModel income)
        {
            return new UserIncomesListItemViewModel(
                income.IncomeId,
                income.Source,
                income.Amount,
                income.DateReceived,
                income.Id);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.Count > 0)
            {
                Guid userId = (Guid)query["UserId"];

                UserId = userId;
            }
        }

        public async void Receive(IncomeAddedOrChangedMessage message)
        {
            Incomes.Clear();
            await GetIncomesForDateRange(UserId, StartDate, EndDate);
        }
    }
}
