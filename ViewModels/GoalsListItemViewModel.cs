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
        private DateTime _date;
        [ObservableProperty]
        private decimal _amount;
        [ObservableProperty]
        private string _description;
        [ObservableProperty]
        private Guid _userId;

        public GoalsListItemViewModel(int goalId, DateTime date, decimal amount, string description, Guid userId)
        {
            GoalId = goalId;
            Date = date;
            Amount = amount;
            Description = description;
            UserId = userId;
        }
    }
}
