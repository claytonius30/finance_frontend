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
    public partial class UserIncomesListItemViewModel : ObservableObject, IRecipient<StatusChangedMessage>
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
        private int _userId;

        public UserIncomesListItemViewModel(int incomeId, string source, decimal amount, DateTime dateReceived, int userId)
        {
            IncomeId = incomeId;
            Source = source;
            Amount = amount;
            DateReceived = dateReceived;
            UserId = userId;

            WeakReferenceMessenger.Default.Register(this);
        }

        public void Receive(StatusChangedMessage message)
        {
            if (message.Id == IncomeId)
            {

            }
        }
    }
}
