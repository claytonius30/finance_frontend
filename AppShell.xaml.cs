using FinanceMAUI.Views;

namespace FinanceMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("incomes", typeof(UserIncomesOverviewPage));
        }
    }
}
