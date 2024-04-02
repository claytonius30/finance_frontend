using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinanceMAUI.Models;
using FinanceMAUI.Services;
using FinanceMAUI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Provider.CalendarContract;

namespace FinanceMAUI.ViewModels
{
    public partial class UserDetailViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        private string _firstName;
        [ObservableProperty]
        private string _lastName;
        [ObservableProperty]
        private string _fullName;
        //public string FullName
        //{
        //    get => FirstName + " " + LastName;
        //}

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(HideBalanceCommand))]
        public double? _balance;

        [ObservableProperty]
        public bool _checkSummary;

        [ObservableProperty]
        private DateTime? _date = DateTime.Now;
        //public DateTime _date {  get; set; } = DateTime.Now;

        [RelayCommand]
        private void HideBalance() => Balance = null;

        [RelayCommand]
        private async Task ViewIncomes()
        {
            //Shell.Current.GoToAsync("incomes");

            //Id = 1;

            await _navigationService.GoToUserIncomes(Id);
        }

        public UserDetailViewModel(IUserService userService, INavigationService navigationService) 
        {
            _userService = userService;
            _navigationService = navigationService;

            Id = 3;
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
                    await GetUser(Id);
                    await CheckFinancialSummary(Id);
                    if (CheckSummary == true)
                    {
                        await GetCurrentBalance(Id);
                    }
                });
        }

        private async Task GetUser(int id)
        {
            var @user = await _userService.GetUser(id);

            if (@user != null)
            {
                MapUserData(@user);
            }
        }

        private async Task CheckFinancialSummary(int id)
        {
            bool checkSummary = await _userService.CheckFinancialSummary(id);

            MapUserSummary(checkSummary);
        }

        private async Task GetCurrentBalance(int id)
        {
            var balance = await _userService.GetCurrentBalance(id);

            //if (balance != null)
            //{
                MapUserBalance(balance);
            //}
        }

        private void MapUserData(UserModel @user)
        {
            Id = @user.Id;
            FirstName = @user.FirstName;
            LastName = @user.LastName;
            FullName = FirstName + " " + LastName;
        }

        private void MapUserBalance(double balance)
        {
            Balance = balance;
        }

        private void MapUserSummary(bool checkSummary)
        {
            CheckSummary = checkSummary;
        }
    }
}
