using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Services
{
    public class NavigationService : INavigationService
    {
        public async Task GoToUserIncomes(int userId)
        {
            var parameters = new Dictionary<string, object> { { "UserId", userId} };
            await Shell.Current.GoToAsync("incomes", parameters);
        }

        public async Task GoToIncomeDetail(int userId, int incomeId)
        {
            var parameters = new Dictionary<string, object> { { "userId", userId }, { "incomeId", incomeId } };
            await Shell.Current.GoToAsync("income", parameters);
        }
    }
}
