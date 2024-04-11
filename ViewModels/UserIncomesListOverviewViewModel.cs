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
    public partial class UserIncomesListOverviewViewModel : ViewModelBase, IQueryAttributable, IRecipient<IncomeAddedOrChangedMessage>
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;
        [ObservableProperty]
        private Guid _userId;


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

        public UserIncomesListOverviewViewModel(IUserService userService, INavigationService navigationService)
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
                    await GetIncomes(UserId);
                });
        }

        private async Task GetIncomes(Guid id)
        {
            List<IncomeModel> incomes = await _userService.GetIncomes(id);
            List<UserIncomesListItemViewModel> listItems = new();
            foreach (var @income in incomes)
            {
                listItems.Insert(0, MapIncomeModelToUserIncomesListItemViewModel(@income));
            }

            Incomes.Clear();
            Incomes = listItems.ToObservableCollection();
        }

        private UserIncomesListItemViewModel MapIncomeModelToUserIncomesListItemViewModel(IncomeModel @income)
        {
            return new UserIncomesListItemViewModel(
                @income.IncomeId,
                @income.Source,
                @income.Amount,
                @income.DateReceived,
                @income.Id);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Guid userId = (Guid) query["UserId"];

            UserId = userId;
            //await GetIncomes(Id);
        }

        public async void Receive(IncomeAddedOrChangedMessage message)
        {
            Incomes.Clear();
            await GetIncomes(UserId);
        }
    }
}
