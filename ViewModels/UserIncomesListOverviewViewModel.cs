using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public partial class UserIncomesListOverviewViewModel : ViewModelBase, IQueryAttributable
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;
        [ObservableProperty]
        private int _id;

        
        [ObservableProperty]
        private ObservableCollection<UserIncomesListItemViewModel> _incomes = new();
        [ObservableProperty]
        private UserIncomesListItemViewModel _selectedIncome;

        [RelayCommand]
        private async Task NavigateToSelectedDetail()
        {
            if (SelectedIncome is not null)
            {
                await _navigationService.GoToIncomeDetail(Id, SelectedIncome.IncomeId);
                SelectedIncome = null;
            }
        }

        public UserIncomesListOverviewViewModel(IUserService userService, INavigationService navigationService)
        {
            _userService = userService;
            _navigationService = navigationService;

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
                    await GetIncomes(Id);
                });
        }

        private async Task GetIncomes(int id)
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
                @income.DateReceived);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            int userId = (int) query["UserId"];

            Id = userId;
            //await GetIncomes(Id);
        }
    }
}
