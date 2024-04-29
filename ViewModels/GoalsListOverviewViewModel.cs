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
    public partial class GoalsListOverviewViewModel : ViewModelBase, IQueryAttributable
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private Guid _userId;

        [ObservableProperty]
        private ObservableCollection<GoalsListItemViewModel> _goals = new();

        [ObservableProperty]
        private GoalsListItemViewModel? _selectedGoal;

        [RelayCommand]
        private async Task NavigateToAddGoal()
            => await _navigationService.GoToAddGoal(UserId);

        [RelayCommand]
        private async Task NavigateToSelectedDetail()
        {
            if (SelectedGoal is not null)
            {
                await _navigationService.GoToGoalDetail(UserId, SelectedGoal.GoalId);
                SelectedGoal = null;
            }
        }

        [RelayCommand]
        private async Task Back()
        {
            await _navigationService.GoToUserDetail(UserId);
        }

        public GoalsListOverviewViewModel(IUserService userService, INavigationService navigationService)
        {
            _userService = userService;
            _navigationService = navigationService;
        }

        public override async Task LoadAsync()
        {
            await Loading(
                async () =>
                {
                    await GetGoals(UserId);
                });
        }

        private async Task GetGoals(Guid id)
        {
            List<GoalModel> goals = await _userService.GetGoals(id);
            List<GoalsListItemViewModel> listItems = new();
            foreach (var goal in goals)
            {
                listItems.Insert(0, MapGoalModelToGoalsListItemViewModel(goal));
            }

            Goals.Clear();
            Goals = listItems.ToObservableCollection();
        }

        private GoalsListItemViewModel MapGoalModelToGoalsListItemViewModel(GoalModel goal)
        {
            return new GoalsListItemViewModel(
                goal.GoalId,
                goal.Date,
                goal.Amount,
                goal.Description,
                goal.Status,
                goal.Id);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.Count > 0)
            {
                Guid userId = (Guid)query["UserId"];

                UserId = userId;
            }
        }
    }
}
