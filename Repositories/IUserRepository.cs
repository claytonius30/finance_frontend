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

        Task<LockoutInfoModel> GetLockoutEnd(string email);
        Task<Guid> GetGuid(string email);

        Task<UserModel?> GetUser(Guid id);

        Task<bool> PutUser(UserModel model);

        Task<decimal> GetCurrentBalance(Guid id);

        Task<IncomeModel?> GetIncome(Guid userId, int incomeId);

        Task<List<IncomeModel>> GetIncomes(Guid userId);

        Task<List<TransactionModel>> GetAllTransactions(Guid userId);

        Task<List<TransactionModel>> GetTransactionsForDateRange(Guid userId, DateTime startDate, DateTime endDate);

        Task<bool> CheckFinancialSummary(Guid id);

        Task<bool> CreateIncome(IncomeModel model);

        Task<bool> EditIncome(IncomeModel model);

        Task<bool> DeleteIncome(Guid userId, int incomeId);
    }
}
