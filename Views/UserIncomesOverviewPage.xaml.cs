using FinanceMAUI.ViewModels;

namespace FinanceMAUI.Views;

public partial class UserIncomesOverviewPage : ContentPageBase
{
	public UserIncomesOverviewPage(UserIncomesListOverviewViewModel vm)
	{
        InitializeComponent();
		BindingContext = vm;
	}
}