using FinanceMAUI.ViewModels;
namespace FinanceMAUI.Views;

public partial class ExpenseAddEditPage : ContentPageBase
{
	public ExpenseAddEditPage(ExpenseAddEditViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}