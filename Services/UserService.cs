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

        public Task<UserModel?> GetUser(int id)
            => _userRepository.GetUser(id);

        public Task<decimal> GetCurrentBalance(int id)
            => _userRepository.GetCurrentBalance(id);

        public Task<IncomeModel?> GetIncome(int userId, int incomeId)
            => _userRepository.GetIncome(userId, incomeId);

        public Task<List<IncomeModel>> GetIncomes(int id)
            => _userRepository.GetIncomes(id);

        public Task<bool> CheckFinancialSummary(int id)
            => _userRepository.CheckFinancialSummary(id);

        public Task<bool> CreateIncome(IncomeModel model)
            => _userRepository.CreateIncome(model);

        public Task<bool> EditIncome(IncomeModel model)
            => _userRepository.EditIncome(model);

        public Task<bool> DeleteIncome(int userId, int incomeId)
        => _userRepository.DeleteIncome(userId, incomeId);
    }
}
