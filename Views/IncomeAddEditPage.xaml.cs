using FinanceMAUI.ViewModels;

namespace FinanceMAUI.Views;

public partial class IncomeAddEditPage : ContentPageBase
{
	public IncomeAddEditPage(IncomeAddEditViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}