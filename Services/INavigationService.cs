using FinanceMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Services
{
    public interface INavigationService
    {
        Task GoToUserIncomes(int userId);
        Task GoToEditIncome(IncomeModel detailModel);
        Task GoToIncomeDetail(int userId, int incomeId);
        Task GoToAddIncome(int userId);
        Task GoToOverview();
        Task GoBack();
    }
}
