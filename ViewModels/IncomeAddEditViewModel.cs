using CommunityToolkit.Mvvm.ComponentModel;
using FinanceMAUI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using FinanceMAUI.Services;
using FinanceMAUI.Models;
using Android.Mtp;

namespace FinanceMAUI.ViewModels
{
    public partial class IncomeAddEditViewModel : ViewModelBase, IQueryAttributable
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;

        public IncomeModel? incomeDetail;

        [ObservableProperty]
        private string _pageTitle = default!;

        [ObservableProperty]
        private int _incomeId;

        [ObservableProperty]
        private string? _source;

        [ObservableProperty]
        private decimal _amount;

        [ObservableProperty]
        private DateTime _dateReceived = DateTime.Now;

        [ObservableProperty]
        private DateTime _minDate = DateTime.Now.AddYears(-4);

        [ObservableProperty]
        private int _userId;

        //[ObservableProperty]
        //[NotifyCanExecuteChangedFor(nameof(AddIncomeCommand))]
        //private string _addedIncome = default!;

        //[RelayCommand(CanExecute = nameof(CanAddIncome))]
        //private void AddIncome()
        //{
        //    Incomes.Add(AddedIncome);
        //    AddedIncome = string.Empty;
        //}

        //private bool CanAddIncome() => !string.IsNullOrWhiteSpace(AddedIncome);

        //[RelayCommand(CanExecute = nameof(CanSubmitIncome))]
        //private async Task Submit()
        //{
        //}

        private bool CanSubmitEvent() => true;

        public IncomeAddEditViewModel(IUserService userService, INavigationService navigationService)
        {
            _userService = userService;
            _navigationService = navigationService;
        }

        public override async Task LoadAsync()
        {
            await Loading(
                async () =>
                {
                    if (incomeDetail is null && IncomeId > 0)
                    {
                        incomeDetail = await _userService.GetIncome(UserId, IncomeId);
                    }
                    MapIncome(incomeDetail);
                });
        }

        private void MapIncome(IncomeModel? model)
        {
            if (model is not null)
            {
                IncomeId = model.IncomeId;
                Source = model.Source;
                Amount = model.Amount;
                DateReceived = model.DateReceived;
                UserId = model.UserId;
            }

            PageTitle = IncomeId > 0 ? "Edit Income" : "Add Income";
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.Count > 0)
            {
                incomeDetail = query["Income"] as IncomeModel;
            }
        }
    }
}
