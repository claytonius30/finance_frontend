using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinanceMAUI.Models;
using FinanceMAUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.ViewModels
{
    public partial class UserDetailViewModel : ObservableObject
    {
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
        private DateTime? _date = DateTime.Now;
        //public DateTime _date {  get; set; } = DateTime.Now;

        [RelayCommand]
        private void HideBalance() => Balance = null;

        private readonly IUserService _userService;

        public UserDetailViewModel(IUserService userService) 
        {
            _userService = userService;

            Id = 1;
            GetUser(Id);
            GetCurrentBalance(Id);
            //Id = 0;
            //FirstName = "John";
            //LastName = "Smith";
            //Balance = 0;
            //Date = DateTime.Now;
        }

        private async void GetUser(int id)
        {
            var @user = await _userService.GetUser(id);

            if (@user != null)
            {
                MapUserData(@user);
            }
        }

        private async void GetCurrentBalance(int id)
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
    }
}
