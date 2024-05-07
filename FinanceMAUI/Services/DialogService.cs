// Clayton DeSimone
// .NET Applications
// Final Project
// 4/29/2024

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Services
{
    public class DialogService : IDialogService
    {
        public Task<bool> Ask(string title, string message, string trueButtonText = "Yes", string falseButtonText = "No")
        => Shell.Current.DisplayAlert(title, message, trueButtonText, falseButtonText);

        public Task Notify(string title, string message, string buttonText = "OK")
            => Shell.Current.DisplayAlert(title, message, buttonText);
    }
}
