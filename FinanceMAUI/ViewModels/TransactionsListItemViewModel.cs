// Clayton DeSimone
// .NET Applications
// Final Project
// 4/29/2024

using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.ViewModels
{
    public partial class TransactionsListItemViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _type;
        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        private decimal _amount;
        [ObservableProperty]
        private string _description;
        [ObservableProperty]
        private DateTime _date;

        public TransactionsListItemViewModel(string type, int id, decimal amount, string description, DateTime date)
        {
            Type = type;
            Id = id;
            Amount = amount;
            Description = description;
            Date = date;
        }
    }
}
