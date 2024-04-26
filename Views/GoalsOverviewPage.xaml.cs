using FinanceMAUI.ViewModels;

namespace FinanceMAUI.Views;

public partial class GoalsOverviewPage : ContentPageBase
{
	public GoalsOverviewPage(GoalsListOverviewViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}