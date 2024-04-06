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
        Task<UserModel?> GetUser(int id);

        Task<double> GetCurrentBalance(int id);

        Task<IncomeModel?> GetIncome(int userId, int incomeId);

        Task<List<IncomeModel>> GetIncomes(int userId);

        Task<bool> CheckFinancialSummary(int id);
    }
}
