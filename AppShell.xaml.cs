using FinanceMAUI.Views;

namespace FinanceMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("incomes", typeof(UserIncomesOverviewPage));
            Routing.RegisterRoute("income", typeof(IncomeDetailPage));
            Routing.RegisterRoute("income/add", typeof(IncomeAddEditPage));
            Routing.RegisterRoute("income/edit", typeof(IncomeAddEditPage));
        }
    }
}
