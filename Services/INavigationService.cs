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
        Task GoToUserDetail(Guid userId);
        Task GoToUserIncomes(Guid userId);
        Task GoToEditIncome(IncomeModel detailModel);
        Task GoToIncomeDetail(Guid userId, int incomeId);
        Task GoToAddIncome(Guid userId);
        Task GoToOverview();
        Task GoBack();
    }
}
