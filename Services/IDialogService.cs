using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Services
{
    public interface IDialogService
    {
        Task<bool> Ask(string title, string message, string trueButtonText = "Yes", string falseButtonText = "No");
        Task Notify(string title, string message, string buttonText = "OK");
    }
}
