using FinanceMAUI.ViewModels;

namespace FinanceMAUI.Views;

public partial class LoginPage : ContentPageBase
{
    public LoginPage(LoginViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
