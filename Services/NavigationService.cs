using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Services
{
    public class NavigationService : INavigationService
    {
        //public Task GoToUserIncomes(int userId)
        //    => Shell.Current.GoToAsync("incomes");

        public async Task GoToUserIncomes(int userId)
        {
            var parameters = new Dictionary<string, object> { { "UserId", userId} };
            await Shell.Current.GoToAsync("incomes", parameters);
        }
    }
}
