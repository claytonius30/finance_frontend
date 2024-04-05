using FinanceMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Services
{
    public interface IIncomeService
    {
        Task<IncomeModel?> GetIncome(int id);

        Task<List<IncomeModel>> GetIncomes(int id);
        
    }
}
