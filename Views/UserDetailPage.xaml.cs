using FinanceMAUI.ViewModels;

namespace FinanceMAUI.Views;

public partial class UserDetailPage : ContentPage
{
	public UserDetailPage(UserDetailViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}