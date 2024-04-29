// Clayton DeSimone
// .NET Applications
// Final Project
// 4/29/2024

using FinanceMAUI.ViewModels;
using System;
using Microsoft.Maui.Controls;
using FinanceMAUI.Views;

namespace FinanceMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("register", typeof(RegisterPage));
            Routing.RegisterRoute("user", typeof(UserDetailPage));
            Routing.RegisterRoute("transactions", typeof(TransactionsOverviewPage));
            Routing.RegisterRoute("goals", typeof(GoalsOverviewPage));
            Routing.RegisterRoute("goal", typeof(GoalDetailPage));
            Routing.RegisterRoute("goal/add", typeof(GoalAddEditPage));
            Routing.RegisterRoute("goal/edit", typeof(GoalAddEditPage));
            Routing.RegisterRoute("incomes", typeof(UserIncomesOverviewPage));
            Routing.RegisterRoute("income", typeof(IncomeDetailPage));
            Routing.RegisterRoute("expenses", typeof(UserExpensesOverviewPage));
            Routing.RegisterRoute("expense", typeof(ExpenseDetailPage));
            Routing.RegisterRoute("addincome", typeof(IncomeAddEditPage));
            Routing.RegisterRoute("editincome", typeof(IncomeAddEditPage));
            Routing.RegisterRoute("addexpense", typeof(ExpenseAddEditPage));
            Routing.RegisterRoute("editexpense", typeof(ExpenseAddEditPage));
        }
    }
}
