// Clayton DeSimone
// .NET Applications
// Final Project
// 4/29/2024

using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.ViewModels
{
    public partial class GoalsListItemViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _goalId;
        [ObservableProperty]
        private DateTime _setDate;
        [ObservableProperty]
        private DateTime _goalDate;
        [ObservableProperty]
        private decimal _amount;
        [ObservableProperty]
        private string _description;
        [ObservableProperty]
        private string _status;
        [ObservableProperty]
        private Guid _userId;

        [ObservableProperty]
        private string? _goalColor;

        public GoalsListItemViewModel(int goalId, DateTime setDate, DateTime goalDate, decimal amount, string description, string status, Guid userId)
        {
            GoalId = goalId;
            SetDate = setDate;
            GoalDate = goalDate;
            Amount = amount;
            Description = description;
            Status = status;
            UserId = userId;
        }
    }
}
