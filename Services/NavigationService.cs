﻿using FinanceMAUI.Models;
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

        public async Task GoToEditIncome(IncomeModel detailModel)
        {
            var navigationParameter = new ShellNavigationQueryParameters
            {
                { "Income", detailModel }
            };

            await Shell.Current.GoToAsync("income/edit", navigationParameter);
        }

        public async Task GoToIncomeDetail(int userId, int incomeId)
        {
            var parameters = new Dictionary<string, object> { { "UserId", userId }, { "IncomeId", incomeId } };
            await Shell.Current.GoToAsync("income", parameters);
        }

        public async Task GoToAddIncome(int userId)
        {
            var parameters = new Dictionary<string, object> { { "UserId", userId } };
            await Shell.Current.GoToAsync("income/add", parameters);
        }

        public Task GoToOverview()
            => Shell.Current.GoToAsync("//userview");

        public Task GoBack()
            => Shell.Current.GoToAsync("..");
    }
}
