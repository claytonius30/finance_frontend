// Clayton DeSimone
// .NET Applications
// Final Project
// 4/29/2024

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FinanceMAUI.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.ViewModels
{
    public partial class UserIncomesListItemViewModel : ObservableObject
    {
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

        public UserIncomesListItemViewModel(int incomeId, string source, decimal amount, DateTime dateReceived, Guid userId)
        {
            IncomeId = incomeId;
            Source = source;
            Amount = amount;
            DateReceived = dateReceived;
            UserId = userId;
        }
    }
}
