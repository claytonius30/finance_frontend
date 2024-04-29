// Clayton DeSimone
// .NET Applications
// Final Project
// 4/29/2024

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