using FinanceMAUI.Views;

namespace FinanceMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("user", typeof(UserDetailPage));
            Routing.RegisterRoute("incomes", typeof(UserIncomesOverviewPage));
            Routing.RegisterRoute("income", typeof(IncomeDetailPage));
            Routing.RegisterRoute("income/add", typeof(IncomeAddEditPage));
            Routing.RegisterRoute("income/edit", typeof(IncomeAddEditPage));
            
            Routing.RegisterRoute(nameof(WeatherForecastPage), typeof(WeatherForecastPage));
        }
    }
}
