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

namespace FinanceMAUI.ViewModels
{
    public partial class GoalDetailViewModel : ViewModelBase, IQueryAttributable
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        [ObservableProperty]
        private int _goalId;
        [ObservableProperty]
        private DateTime _date;
        [ObservableProperty]
        private decimal _amount;
        [ObservableProperty]
        private string _description;
        [ObservableProperty]
        private string _status;

        [ObservableProperty]
        private Guid _userId;

        [ObservableProperty]
        private double _daysUntilGoal;

        [ObservableProperty]
        private decimal _currentBalanceDifference;

        [ObservableProperty]
        private string _balanceColor;

        [RelayCommand]
        private async Task NavigateToEditGoal()
        {
            var detailModel = MapToGoalModel(this);
            await _navigationService.GoToEditGoal(detailModel);
        }

        [RelayCommand]
        public async Task DeleteGoal()
        {
            if (await _dialogService.Ask(
                    "Delete goal",
                    "Are you sure you want to delete this goal?"))
            {
                if (await _userService.DeleteGoal(UserId, GoalId))
                {
                    await _navigationService.GoToGoals(UserId);
                    await _dialogService.Notify("Success", $"The goal '{Description}' is deleted.");
                }
            }
        }

        [RelayCommand]
        private async Task Back()
        {
            await _navigationService.GoToGoals(UserId);
        }

        public GoalDetailViewModel(IUserService userService, INavigationService navigationService, IDialogService dialogService)
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
                    await GetGoal(UserId, GoalId);
                    CalculateDaysDifference();
                    await CalculateBalanceDifference();
                });
        }

        private void CalculateDaysDifference()
        {
            DateTime now = DateTime.Now;
            TimeSpan daysDifference;
            daysDifference = Date.Subtract(now);
            double roundedDifference = Math.Round(daysDifference.TotalDays, 1);
            DaysUntilGoal = roundedDifference;
        }

        private async Task CalculateBalanceDifference()
        {
            var currentBalance = await _userService.GetCurrentBalance(UserId);
            decimal tempBalance = (decimal)(Amount - currentBalance);
            if (-tempBalance > 0)
            {
                BalanceColor = "Green";
            }
            else if (-tempBalance < 0)
            {
                BalanceColor = "Red";
            }
            else
            {
                BalanceColor = "Black";
            }
            CurrentBalanceDifference = -tempBalance;
        }

        private async Task GetGoal(Guid userId, int goalId)
        {
            var goal = await _userService.GetGoal(userId, goalId);

            if (goal != null)
            {
                MapGoalData(goal);
            }
        }

        private void MapGoalData(GoalModel goal)
        {
            GoalId = goal.GoalId;
            Date = goal.Date;
            Amount = goal.Amount;
            Description = goal.Description;
            Status = goal.Status;
            UserId = goal.Id;
        }

        private GoalModel MapToGoalModel(GoalDetailViewModel goalViewModel)
        {
            return new GoalModel
            {
                GoalId = goalViewModel.GoalId,
                Date = goalViewModel.Date,
                Amount = goalViewModel.Amount,
                Description = goalViewModel.Description,
                Status = goalViewModel.Status,
                Id = goalViewModel.UserId
            };
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Guid userId = (Guid)query["UserId"];
            int goalId = (int)query["GoalId"];

            UserId = userId;
            GoalId = goalId;
        }
    }
}
