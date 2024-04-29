using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.ViewModels
{
    public partial class UserExpensesListItemViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _expenseId;
        [ObservableProperty]
        private string _category;
        [ObservableProperty]
        private decimal _amount;
        [ObservableProperty]
        private DateTime _dateIncurred;
        [ObservableProperty]
        private Guid _userId;

        public UserExpensesListItemViewModel(int expenseId, string category, decimal amount, DateTime dateIncurred, Guid userId)
        {
            _expenseId = expenseId;
            _category = category;
            _amount = amount;
            _dateIncurred = dateIncurred;
            _userId = userId;
        }
    }
}
