using FinanceMAUI.Models;
using FinanceMAUI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Services
{
    class IncomeService : IIncomeService
    {
        private readonly IUserRepository _userRepository;

        public IncomeService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<UserModel?> GetUser(Guid id)
            => _userRepository.GetUser(id);

        public Task<decimal?> GetCurrentBalance(Guid id)
            => _userRepository.GetCurrentBalance(id);

        public Task<List<IncomeModel>> GetIncomes(Guid id)
            => _userRepository.GetIncomes(id);

        public Task<bool> CheckFinancialSummary(Guid id)
            => _userRepository.CheckFinancialSummary(id);

        public Task<IncomeModel?> GetIncome(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<List<IncomeModel>> IIncomeService.GetIncomes(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
