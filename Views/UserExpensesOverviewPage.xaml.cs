using FinanceMAUI.ViewModels;
namespace FinanceMAUI.Views;

public partial class UserExpensesOverviewPage : ContentPageBase
{
	public UserExpensesOverviewPage(UserExpensesListOverviewViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}