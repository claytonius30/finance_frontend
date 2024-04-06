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

namespace FinanceMAUI.ViewModels
{
    public partial class IncomeDetailViewModel : ViewModelBase, IQueryAttributable
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private int _incomeId;
        [ObservableProperty]
        private string _source;
        [ObservableProperty]
        private decimal _amount;
        [ObservableProperty]
        private DateTime _dateReceived;
        [ObservableProperty]
        private int _userId;


        public IncomeDetailViewModel(IUserService userService, INavigationService navigationService)
        {
            _userService = userService;
            _navigationService = navigationService;

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

        private async Task GetIncome(int userId, int incomeId)
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
        }

        public  void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            int userId = (int)query["UserId"];
            int incomeId = (int)query["IncomeId"];

            UserId = userId;
            IncomeId = incomeId;
        }
    }
}
