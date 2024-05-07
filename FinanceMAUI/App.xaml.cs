// Clayton DeSimone
// .NET Applications
// Final Project
// 4/29/2024

using FinanceMAUI.Views;

namespace FinanceMAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
