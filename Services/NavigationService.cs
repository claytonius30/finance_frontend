using FinanceMAUI.Models;
using FinanceMAUI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Services
{
    public class NavigationService : INavigationService
    {
        public Task GoToRegister()
            => Shell.Current.GoToAsync("register");

        public async Task GoToUserDetail(Guid userId)
        {
            var parameters = new Dictionary<string, object> { { "UserId", userId } };
            await Shell.Current.GoToAsync("user", parameters);
        }

        public async Task GoToUserIncomes(Guid userId)
        {
            var parameters = new Dictionary<string, object> { { "UserId", userId} };
            await Shell.Current.GoToAsync("incomes", parameters);
        }

        public async Task GoToUserExpenses(Guid userId)
        {
            var parameters = new Dictionary<string, object> { { "UserId", userId } };
            await Shell.Current.GoToAsync("expenses", parameters);
        }

        public async Task GoToTransactions(Guid userId)
        {
            var parameters = new Dictionary<string, object> { { "UserId", userId } };
            await Shell.Current.GoToAsync("transactions", parameters);
        }

        public async Task GoToEditIncome(IncomeModel detailModel)
        {
            var navigationParameter = new ShellNavigationQueryParameters
            {
                { "Income", detailModel }
            };

            await Shell.Current.GoToAsync("editincome", navigationParameter);
        }

        public async Task GoToEditExpense(ExpenseModel detailModel)
        {
            var navigationParameter = new ShellNavigationQueryParameters
            {
                { "Expense", detailModel }
            };

            await Shell.Current.GoToAsync("editexpense", navigationParameter);
        }

        public async Task GoToEditGoal(GoalModel detailModel)
        {
            var navigationParameter = new ShellNavigationQueryParameters
            {
                { "Goal", detailModel }
            };

            await Shell.Current.GoToAsync("goal/edit", navigationParameter);
        }

        public async Task GoToGoals(Guid userId)
        {
            var parameters = new Dictionary<string, object> { { "UserId", userId } };
            await Shell.Current.GoToAsync("goals", parameters);
        }

        public async Task GoToGoalDetail(Guid userId, int goalId)
        {
            var parameters = new Dictionary<string, object> { { "UserId", userId }, { "GoalId", goalId } };
            await Shell.Current.GoToAsync("goal", parameters);
        }

        public async Task GoToIncomeDetail(Guid userId, int incomeId)
        {
            var parameters = new Dictionary<string, object> { { "UserId", userId }, { "IncomeId", incomeId } };
            await Shell.Current.GoToAsync("income", parameters);
        }

        public async Task GoToExpenseDetail(Guid userId, int expenseId)
        {
            var parameters = new Dictionary<string, object> { { "UserId", userId }, { "ExpenseId", expenseId } };
            await Shell.Current.GoToAsync("expense", parameters);
        }

        public async Task GoToAddIncome(Guid userId)
        {
            var parameters = new Dictionary<string, object> { { "UserId", userId } };
            await Shell.Current.GoToAsync("addincome", parameters);
        }

        public async Task GoToAddExpense(Guid userId)
        {
            var parameters = new Dictionary<string, object> { { "UserId", userId } };
            await Shell.Current.GoToAsync("addexpense", parameters);
        }

        public async Task GoToAddGoal(Guid userId)
        {
            var parameters = new Dictionary<string, object> { { "UserId", userId } };
            await Shell.Current.GoToAsync("goal/add", parameters);
        }

        public Task GoToOverview()
            => Shell.Current.GoToAsync("//login");

        public Task GoBack()
            => Shell.Current.GoToAsync("..");
    }
}
