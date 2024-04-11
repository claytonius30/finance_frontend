using FinanceMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel?> GetUser(Guid id);

        Task<decimal> GetCurrentBalance(Guid id);

        Task<IncomeModel?> GetIncome(Guid userId, int incomeId);

        Task<List<IncomeModel>> GetIncomes(Guid userId);

        Task<bool> CheckFinancialSummary(Guid id);

        Task<bool> CreateIncome(IncomeModel model);

        Task<bool> EditIncome(IncomeModel model);

        Task<bool> DeleteIncome(Guid userId, int incomeId);
    }
}
