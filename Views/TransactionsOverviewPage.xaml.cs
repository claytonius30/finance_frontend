using FinanceMAUI.ViewModels;
namespace FinanceMAUI.Views;

public partial class TransactionsOverviewPage : ContentPageBase
{
	public TransactionsOverviewPage(TransactionsListOverviewViewModel vm)
	{
        InitializeComponent();
        BindingContext = vm;
    }
}