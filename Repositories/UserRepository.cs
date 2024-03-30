using FinanceMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinanceMAUI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UserModel?> GetUser(int id)
        {
            using HttpClient client = _httpClientFactory.CreateClient("FinanceTrackerApiClient");

            try
            {
                UserModel? @user = await client.GetFromJsonAsync<UserModel>(
                    $"api/User/{id}",
                    new JsonSerializerOptions(JsonSerializerDefaults.Web));

                return @user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<double> GetCurrentBalance(int id)
        {
            using HttpClient client = _httpClientFactory.CreateClient("FinanceTrackerApiClient");

            //try
            //{
                double balance = await client.GetFromJsonAsync<double>(
                    $"api/User/{id}/GetCurrentBalance",
                    new JsonSerializerOptions(JsonSerializerDefaults.Web));

                return balance;
            //}
            //catch (Exception)
            //{
            //    return null;
            //}
        }
    }
}
