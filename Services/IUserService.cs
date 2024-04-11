using FinanceMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Services
{
    public interface IUserService
    {
        Task<UserModel?> GetUser(Guid id);

        Task<decimal> GetCurrentBalance(Guid id);

        Task<IncomeModel?> GetIncome(Guid userid, int incomeId);

        Task<List<IncomeModel>> GetIncomes(Guid id);

        Task<bool> CheckFinancialSummary(Guid id);

        Task<bool> CreateIncome(IncomeModel model);

        Task<bool> EditIncome(IncomeModel model);

        Task<bool> DeleteIncome(Guid userId, int incomeId);
    }
}
