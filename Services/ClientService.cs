// Clayton DeSimone
// .NET Applications
// Final Project
// 4/29/2024

using CommunityToolkit.Mvvm.Messaging;
using FinanceMAUI.Messages;
using FinanceMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinanceMAUI.Services
{
    public class ClientService : IClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IDialogService _dialogService;

        public ClientService(IHttpClientFactory httpClientFactory, IDialogService dialogService)
        {
            _httpClientFactory = httpClientFactory;
            _dialogService = dialogService;
        }

        public async Task Register(RegisterModel model)
        {
            var httpClient = _httpClientFactory.CreateClient("custom-httpclient");
            var result = await httpClient.PostAsJsonAsync("/register", model);
            if (result.IsSuccessStatusCode)
            {
                var result2 = await httpClient.PostAsJsonAsync("/login", model);
                var response = await result2.Content.ReadFromJsonAsync<LoginResponseModel>();

                if (result2 is not null)
                {
                    var serializeResponse = JsonSerializer.Serialize(
                        new LoginResponseModel()
                        {
                            AccessToken = response.AccessToken,
                            RefreshToken = response.RefreshToken,
                            UserName = model.Email
                        });
                    var serializeResponse2 = JsonSerializer.Serialize(model.Password);
                    await SecureStorage.Default.SetAsync("Authentication", serializeResponse);
                    await SecureStorage.Default.SetAsync("RegPW", serializeResponse2);
                }
                Console.WriteLine($"{model.Email} Successfully Registered");
            }
            else
            {
                WeakReferenceMessenger.Default.Send(new RegisterMessage());
            }
            Console.WriteLine("Registration Unsuccessful");
        }

        public async Task Login(LoginModel model)
        {
            var httpClient = _httpClientFactory.CreateClient("custom-httpclient");
            var result = await httpClient.PostAsJsonAsync("/login", model);
            var response = await result.Content.ReadFromJsonAsync<LoginResponseModel>();
            
            if (result.IsSuccessStatusCode)
            {
                var serializeResponse = JsonSerializer.Serialize(
                    new LoginResponseModel()
                    {
                        AccessToken = response.AccessToken,
                        RefreshToken = response.RefreshToken,
                        UserName = model.Email
                    });
                await SecureStorage.Default.SetAsync("Authentication", serializeResponse);
                Console.WriteLine($"{model.Email} Successfully Logged In");
            }
            else
            {
                WeakReferenceMessenger.Default.Send(new LoginMessage());
            }
            Console.WriteLine("Login Unsuccessful");
        }
    }
}
