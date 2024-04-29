// Clayton DeSimone
// .NET Applications
// Final Project
// 4/29/2024

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