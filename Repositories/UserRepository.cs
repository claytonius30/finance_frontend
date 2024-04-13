using FinanceMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
//using Windows.System;

namespace FinanceMAUI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Guid> GetGuid(string email)
        {
            using HttpClient client = _httpClientFactory.CreateClient("custom-httpclient");

            //try
            //{
            Guid userId = await client.GetFromJsonAsync<Guid>(
                $"api/User/{email}/GetGuid",
                new JsonSerializerOptions(JsonSerializerDefaults.Web));

            return userId;
            //}
            //catch (Exception)
            //{
            //    return null;
            //}
        }

        public async Task<UserModel?> GetUser(Guid id)
        {
            using HttpClient client = _httpClientFactory.CreateClient("custom-httpclient");

            try
            {
                UserModel? user = await client.GetFromJsonAsync<UserModel>(
                    $"api/User/{id}",
                    new JsonSerializerOptions(JsonSerializerDefaults.Web));

                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IncomeModel?> GetIncome(Guid userId, int incomeId)
        {
            using HttpClient client = _httpClientFactory.CreateClient("custom-httpclient");

            try
            {
                IncomeModel? income = await client.GetFromJsonAsync<IncomeModel>(
                    $"api/User/{userId}/GetIncome/{incomeId}",
                    new JsonSerializerOptions(JsonSerializerDefaults.Web));

                return income;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<decimal> GetCurrentBalance(Guid id)
        {
            using HttpClient client = _httpClientFactory.CreateClient("custom-httpclient");

            //try
            //{
                decimal balance = await client.GetFromJsonAsync<decimal>(
                    $"api/User/{id}/GetCurrentBalance",
                    new JsonSerializerOptions(JsonSerializerDefaults.Web));

                return balance;
            //}
            //catch (Exception)
            //{
            //    return null;
            //}
        }

        public async Task<List<IncomeModel>> GetIncomes(Guid userId)
        {
            using HttpClient client = _httpClientFactory.CreateClient("custom-httpclient");

            try
            {
                List<IncomeModel>? incomes = await client.GetFromJsonAsync<List<IncomeModel>>(
                    $"api/User/{userId}/GetIncomes",
                    new JsonSerializerOptions(JsonSerializerDefaults.Web));

                return incomes ?? new List<IncomeModel>();
            }
            catch (Exception)
            {
                return new List<IncomeModel>();
            }
        }

        public async Task<bool> CheckFinancialSummary(Guid id)
        {
            using HttpClient client = _httpClientFactory.CreateClient   ("custom-httpclient");

            bool checkIfExist = await client.GetFromJsonAsync<bool>(
                    $"api/User/{id}/CheckFinancialSummary",
                    new JsonSerializerOptions(JsonSerializerDefaults.Web));

            return checkIfExist;
        }

        public async Task<bool> CreateIncome(IncomeModel model)
        {
            using HttpClient client = _httpClientFactory.CreateClient("custom-httpclient");

            try
            {
                var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"api/User/{model.Id}/AddIncome", content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        public async Task<bool> EditIncome(IncomeModel model)
        {
            using HttpClient client = _httpClientFactory.CreateClient("custom-httpclient");

            try
            {
                var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"api/User/{model.Id}/UpdateIncome/{model.IncomeId}", content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        public async Task<bool> DeleteIncome(Guid userId, int incomeId)
        {
            using HttpClient client = _httpClientFactory.CreateClient("custom-httpclient");

            try
            {
                var response = await client.DeleteAsync($"api/User/{userId}/DeleteIncome/{incomeId}");
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }
    }
}
