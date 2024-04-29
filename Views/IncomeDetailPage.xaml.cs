using FinanceMAUI.ViewModels;
namespace FinanceMAUI.Views;

public partial class IncomeDetailPage : ContentPageBase
{
	public IncomeDetailPage(IncomeDetailViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}