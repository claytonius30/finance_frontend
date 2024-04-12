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
    public class ClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        //private AndroidHttpMessageHandler = private new AndroidHttpMessageHandler();

        public ClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task Register(RegisterModel model)
        {
            // model.Email = "maui@gmail.com"; model.Password = "Maui@123";
            var httpClient = _httpClientFactory.CreateClient("custom-httpclient");
            var result = await httpClient.PostAsJsonAsync("/register", model);
            if (result.IsSuccessStatusCode)
            {
                await Shell.Current.DisplayAlert("Alert", "successfully Register", "Ok");
            }
            await Shell.Current.DisplayAlert("Alert", result.ReasonPhrase, "Ok");
        }

        public async Task Login(LoginModel model)
        {
            // model.Email = "maui@gmail.com"; model.Password = "Maui@123";
            var httpClient = _httpClientFactory.CreateClient("custom-httpclient");
            var result = await httpClient.PostAsJsonAsync("/login", model);
            var response = await result.Content.ReadFromJsonAsync<LoginResponseModel>();
            if (result is not null)
            {
                var serializeResponse = JsonSerializer.Serialize(
                    new LoginResponseModel()
                    {
                        AccessToken = response.AccessToken,
                        RefreshToken = response.RefreshToken,
                        UserName = model.Email
                    });
                await SecureStorage.Default.SetAsync("Authentication", serializeResponse);
            }
        }




        public async Task<WeatherForecast[]> GetWeatherForeCastData()
        {
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            if (serializedLoginResponseInStorage is null) return null;

            string token = JsonSerializer.Deserialize<LoginResponseModel>(serializedLoginResponseInStorage)!.AccessToken!;
            var httpClient = _httpClientFactory.CreateClient("custom-httpclient");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.GetFromJsonAsync<WeatherForecast[]>("/WeatherForecast");
            return result!;
        }
    }
}
