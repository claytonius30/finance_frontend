using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FinanceMAUI.Messages;
using FinanceMAUI.Models;
using System.ComponentModel;
using System.Text.Json;

namespace FinanceMAUI.Views;

public partial class LogoView : ContentView
{
    public LogoView()
    {
        InitializeComponent();
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        //Task<string> userName = GetUserNameFromSecuredStorage();
        WeakReferenceMessenger.Default.Send(new LogoutMessage());
        await Shell.Current.GoToAsync("//login");
    }

    //private async Task<string> GetUserNameFromSecuredStorage()
    //{
    //    var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
    //    string UserName = JsonSerializer.Deserialize<LoginResponseModel>(serializedLoginResponseInStorage!)!.UserName!;
    //    return UserName;
    //}

}
