using FinanceMAUI.ViewModels;
namespace FinanceMAUI.Views;

public partial class ExpenseDetailPage : ContentPageBase
{
	public ExpenseDetailPage(ExpenseDetailViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}