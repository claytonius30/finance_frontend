using FinanceMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Services
{
    public interface INavigationService
    {
        Task GoToRegister();
        Task GoToUserDetail(Guid userId);
        Task GoToUserIncomes(Guid userId);
        Task GoToTransactions(Guid userId);
        Task GoToEditIncome(IncomeModel detailModel);
        Task GoToEditGoal(GoalModel detailModel);
        Task GoToGoals(Guid userId);
        Task GoToGoalDetail(Guid userId, int goalId);
        Task GoToIncomeDetail(Guid userId, int incomeId);
        Task GoToAddIncome(Guid userId);
        Task GoToAddGoal(Guid userId);
        Task GoToOverview();
        Task GoBack();
    }
}
