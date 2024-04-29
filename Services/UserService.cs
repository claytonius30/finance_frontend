// Clayton DeSimone
// .NET Applications
// Final Project
// 4/29/2024

using FinanceMAUI.Models;
using FinanceMAUI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<LockoutInfoModel> GetLockoutEnd(string email)
            => _userRepository.GetLockoutEnd(email);

        public Task<Guid> GetGuid(string email)
            => _userRepository.GetGuid(email);

        public Task<UserModel?> GetUser(Guid id)
            => _userRepository.GetUser(id);

        public Task<bool> PutUser(UserModel model)
            => _userRepository.PutUser(model);

        public Task<decimal?> GetCurrentBalance(Guid id)
            => _userRepository.GetCurrentBalance(id);

        public Task<decimal?> GetBalanceForDateRange(Guid id, DateTime startDate, DateTime endDate)
            => _userRepository.GetBalanceForDateRange(id,startDate, endDate);

        public Task<List<TransactionModel>> GetAllTransactions(Guid userId)
            => _userRepository.GetAllTransactions(userId);

        public Task<List<TransactionModel>> GetTransactionsForDateRange(Guid userId, DateTime startDate, DateTime endDate)
            => _userRepository.GetTransactionsForDateRange(userId, startDate, endDate);

        public Task<bool> CheckFinancialSummary(Guid id)
            => _userRepository.CheckFinancialSummary(id);

        public Task<IncomeModel?> GetIncome(Guid userId, int incomeId)
            => _userRepository.GetIncome(userId, incomeId);

        public Task<ExpenseModel?> GetExpense(Guid userId, int expenseId)
            => _userRepository.GetExpense(userId, expenseId);

        public Task<List<IncomeModel>> GetIncomesForDateRange(Guid userId, DateTime startDate, DateTime endDate)
            => _userRepository.GetIncomesForDateRange(userId, startDate, endDate);

        public Task<List<IncomeModel>> GetIncomes(Guid id)
            => _userRepository.GetIncomes(id);

        public Task<bool> CreateIncome(IncomeModel model)
            => _userRepository.CreateIncome(model);

        public Task<bool> EditIncome(IncomeModel model)
            => _userRepository.EditIncome(model);

        public Task<bool> DeleteIncome(Guid userId, int incomeId)
        => _userRepository.DeleteIncome(userId, incomeId);

        public Task<List<ExpenseModel>> GetExpenses(Guid userId)
            => _userRepository.GetExpenses(userId);

        public Task<List<ExpenseModel>> GetExpensesForDateRange(Guid userId, DateTime startDate, DateTime endDate)
            => _userRepository.GetExpensesForDateRange(userId, startDate, endDate);

        public Task<bool> CreateExpense(ExpenseModel model)
            => _userRepository.CreateExpense(model);

        public Task<bool> EditExpense(ExpenseModel model)
            => _userRepository.EditExpense(model);

        public Task<bool> DeleteExpense(Guid userId, int expenseId)
            => _userRepository.DeleteExpense(userId, expenseId);

        public Task<List<GoalModel>> GetGoals(Guid userId)
            => _userRepository.GetGoals(userId);

        public Task<GoalModel?> GetGoal(Guid userId, int goalId)
            => _userRepository.GetGoal(userId, goalId);

        public Task<bool> CreateGoal(GoalModel model)
            => _userRepository.CreateGoal(model);

        public Task<bool> EditGoal(GoalModel model)
            => _userRepository.EditGoal(model);

        public Task<bool> DeleteGoal(Guid userId, int goalId)
            => _userRepository.DeleteGoal(userId, goalId);
    }
}
