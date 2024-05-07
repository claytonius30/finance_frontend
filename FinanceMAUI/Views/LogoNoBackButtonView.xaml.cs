// Clayton DeSimone
// .NET Applications
// Final Project
// 4/29/2024

using CommunityToolkit.Mvvm.Messaging;
using FinanceMAUI.Messages;

namespace FinanceMAUI.Views;

public partial class LogoNoBackButtonView : ContentView
{
	public LogoNoBackButtonView()
	{
		InitializeComponent();
	}

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new LogoutMessage());
        await Shell.Current.GoToAsync("//login");
    }
}