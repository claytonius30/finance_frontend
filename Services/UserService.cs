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

        public Task<decimal> GetCurrentBalance(Guid id)
            => _userRepository.GetCurrentBalance(id);

        public Task<IncomeModel?> GetIncome(Guid userId, int incomeId)
            => _userRepository.GetIncome(userId, incomeId);

        public Task<List<IncomeModel>> GetIncomes(Guid id)
            => _userRepository.GetIncomes(id);

        public Task<List<TransactionModel>> GetAllTransactions(Guid userId)
            => _userRepository.GetAllTransactions(userId);

        public Task<List<TransactionModel>> GetTransactionsForDateRange(Guid userId, DateTime startDate, DateTime endDate)
            => _userRepository.GetTransactionsForDateRange(userId, startDate, endDate);

        public Task<bool> CheckFinancialSummary(Guid id)
            => _userRepository.CheckFinancialSummary(id);

        public Task<bool> CreateIncome(IncomeModel model)
            => _userRepository.CreateIncome(model);

        public Task<bool> EditIncome(IncomeModel model)
            => _userRepository.EditIncome(model);

        public Task<bool> DeleteIncome(Guid userId, int incomeId)
        => _userRepository.DeleteIncome(userId, incomeId);
    }
}
