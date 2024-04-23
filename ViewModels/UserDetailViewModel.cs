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
using static Android.Graphics.ImageDecoder;
//using static Android.Provider.CalendarContract;

namespace FinanceMAUI.ViewModels
{
    public partial class UserDetailViewModel : ViewModelBase, IQueryAttributable, IRecipient<IncomeDeletedMessage>
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
        //public DateTime _date {  get; set; } = DateTime.Now;

        [ObservableProperty]
        private string _balanceColor;

        [RelayCommand]
        private void HideBalance() => Balance = null;

        [RelayCommand]
        private async Task ViewIncomes()
        {
            //Shell.Current.GoToAsync("incomes");

            //Id = 1;

            await _navigationService.GoToUserIncomes(Id);
        }

        [RelayCommand]
        private async Task NavigateToAddIncome()
            => await _navigationService.GoToAddIncome(Id);

        public UserDetailViewModel(IUserService userService, INavigationService navigationService, IDialogService dialogService) 
        {
            _userService = userService;
            _navigationService = navigationService;
            _dialogService = dialogService;
                              
            //Id = Guid.Parse("64cfed22-96ed-45a4-3524-08dc5a68942a");
            ////GetUser(Id);
            //GetCurrentBalance(Id);
            //Id = 0;
            //FirstName = "John";
            //LastName = "Smith";
            //Balance = 0;
            //Date = DateTime.Now;

            //WeakReferenceMessenger.Default.Register<IncomeAddedOrChangedMessage>(this);
            WeakReferenceMessenger.Default.Register<IncomeDeletedMessage>(this);
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

            //if (balance != null)
            //{
                MapUserBalance(balance);
            //}
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
                //await GetIncomes(Id);
            }
        }

        //public void Receive(IncomeAddedOrChangedMessage message)
        //{
            
        //}

        public void Receive(IncomeDeletedMessage message)
        {
            //DeletedIncomeAlert(message.Source);
            //_dialogService.Notify("Success", $"The income {message.Source} is deleted.");
        }

        //public void Receive(LoginMessage message)
        //{
            
        //}
    }
}
