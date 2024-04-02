using FinanceMAUI.ViewModels;

namespace FinanceMAUI.Views;

public partial class UserDetailPage : ContentPageBase
{
	public UserDetailPage(UserDetailViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}