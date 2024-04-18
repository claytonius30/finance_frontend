using FinanceMAUI.ViewModels;

namespace FinanceMAUI.Views;

public partial class RegisterPage : ContentPageBase
{
	public RegisterPage(RegisterViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}