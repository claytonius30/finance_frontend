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

        Task<decimal?> GetCurrentBalance(Guid id);

        Task<decimal?> GetBalanceForDateRange(Guid id, DateTime startDate, DateTime endDate);

        Task<List<TransactionModel>> GetAllTransactions(Guid userId);

        Task<List<TransactionModel>> GetTransactionsForDateRange(Guid userId, DateTime startDate, DateTime endDate);

        Task<bool> CheckFinancialSummary(Guid id);

        Task<List<IncomeModel>> GetIncomes(Guid userId);

        Task<List<IncomeModel>> GetIncomesForDateRange(Guid userId, DateTime startDate, DateTime endDate);

        Task<IncomeModel?> GetIncome(Guid userId, int incomeId);

        Task<bool> CreateIncome(IncomeModel model);

        Task<bool> EditIncome(IncomeModel model);

        Task<bool> DeleteIncome(Guid userId, int incomeId);

        Task<List<ExpenseModel>> GetExpenses(Guid userId);

        Task<List<ExpenseModel>> GetExpensesForDateRange(Guid userId, DateTime startDate, DateTime endDate);

        Task<ExpenseModel?> GetExpense(Guid userId, int expenseId);

        Task<bool> CreateExpense(ExpenseModel model);

        Task<bool> EditExpense(ExpenseModel model);

        Task<bool> DeleteExpense(Guid userId, int expenseId);

        Task<List<GoalModel>> GetGoals(Guid userId);

        Task<GoalModel?> GetGoal(Guid userId, int goalId);

        Task<bool> CreateGoal(GoalModel model);

        Task<bool> EditGoal(GoalModel model);

        Task<bool> DeleteGoal(Guid userId, int goalId);
    }
}
