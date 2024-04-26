using FinanceMAUI.ViewModels;

namespace FinanceMAUI.Views;

public partial class GoalAddEditPage : ContentPageBase
{
	public GoalAddEditPage(GoalAddEditViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}