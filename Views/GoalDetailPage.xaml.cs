using FinanceMAUI.ViewModels;

namespace FinanceMAUI.Views;

public partial class GoalDetailPage : ContentPageBase
{
	public GoalDetailPage(GoalDetailViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}