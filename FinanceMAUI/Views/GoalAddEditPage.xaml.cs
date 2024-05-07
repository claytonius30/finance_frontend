// Clayton DeSimone
// .NET Applications
// Final Project
// 4/29/2024

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