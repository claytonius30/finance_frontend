using CommunityToolkit.Mvvm.ComponentModel;
using FinanceMAUI.Services;
using FinanceMAUI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using FinanceMAUI.Models;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FinanceMAUI.Messages;


namespace FinanceMAUI.ViewModels
{
    public partial class IncomeDetailViewModel : ViewModelBase, IQueryAttributable
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        [ObservableProperty]
        private int _incomeId;
        [ObservableProperty]
        private string _source;
        [ObservableProperty]
        private decimal _amount;
        [ObservableProperty]
        private DateTime _dateReceived;
        [ObservableProperty]
        private Guid _userId;

        [RelayCommand]
        private async Task NavigateToEditIncome()
        {
            var detailModel = MapToIncomeModel(this);
            await _navigationService.GoToEditIncome(detailModel);
        }

        [RelayCommand]
        public async Task DeleteIncome()
        {
            if (await _dialogService.Ask(
                    "Delete income",
                    "Are you sure you want to delete this income?"))
            {
                if (await _userService.DeleteIncome(UserId, IncomeId))
                {
                    WeakReferenceMessenger.Default.Send(new IncomeDeletedMessage(UserId, IncomeId));
                    await _navigationService.GoToOverview();
                }
            }
        }

        public IncomeDetailViewModel(IUserService userService, INavigationService navigationService, IDialogService dialogService)
        {
            _userService = userService;
            _navigationService = navigationService;
            _dialogService = dialogService; 

            //UserId = 1;
            //IncomeId = 1;
            //GetUser(Id);
            //GetCurrentBalance(Id);
            //Id = 0;
            //FirstName = "John";
            //LastName = "Smith";
            //Balance = 0;
            //Date = DateTime.Now;
        }

        public override async Task LoadAsync()
        {
            await Loading(
                async () =>
                {
                    await GetIncome(UserId, IncomeId);
                });
        }

        private async Task GetIncome(Guid userId, int incomeId)
        {
            var @income = await _userService.GetIncome(userId, incomeId);

            if (@income != null)
            {
                MapIncomeData(@income);
            }
        }

        private void MapIncomeData(IncomeModel @income)
        {
            IncomeId = @income.IncomeId;
            Source = @income.Source;
            Amount = @income.Amount;
            DateReceived = @income.DateReceived;
            UserId = @income.Id;
        }

        private IncomeModel MapToIncomeModel(IncomeDetailViewModel incomeViewModel)
        {
            return new IncomeModel
            {
                IncomeId = incomeViewModel.IncomeId,
                Source = incomeViewModel.Source,
                Amount = incomeViewModel.Amount,
                DateReceived = incomeViewModel.DateReceived,
                Id = incomeViewModel.UserId
            };
        }

        public  void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Guid userId = (Guid) query["UserId"];
            int incomeId = (int) query["IncomeId"];

            UserId = userId;
            IncomeId = incomeId;
        }
    }
}
