﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Services
{
    public interface INavigationService
    {
        Task GoToUserIncomes(int userId);
        Task GoToIncomeDetail(int userId, int incomeId);
        Task GoToAddIncome();
    }
}