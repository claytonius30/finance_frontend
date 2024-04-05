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

        public async Task<IncomeModel?> GetIncome(int userId, int incomeId)
        {
            using HttpClient client = _httpClientFactory.CreateClient("FinanceTrackerApiClient");

            try
            {
                IncomeModel? @income = await client.GetFromJsonAsync<IncomeModel>(
                    $"api/User/{userId}/GetIncome/{incomeId}",
                    new JsonSerializerOptions(JsonSerializerDefaults.Web));

                return @income;
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

        public async Task<List<IncomeModel>> GetIncomes(int id)
        {
            using HttpClient client = _httpClientFactory.CreateClient("FinanceTrackerApiClient");

            try
            {
                List<IncomeModel>? incomes = await client.GetFromJsonAsync<List<IncomeModel>>(
                    $"api/User/{id}/GetIncomes",
                    new JsonSerializerOptions(JsonSerializerDefaults.Web));

                return incomes ?? new List<IncomeModel>();
            }
            catch (Exception)
            {
                return new List<IncomeModel>();
            }
        }

        public async Task<bool> CheckFinancialSummary(int id)
        {
            using HttpClient client = _httpClientFactory.CreateClient("FinanceTrackerApiClient");

            bool checkIfExist = await client.GetFromJsonAsync<bool>(
                    $"api/User/{id}/CheckFinancialSummary",
                    new JsonSerializerOptions(JsonSerializerDefaults.Web));

            return checkIfExist;
        }
    }
}
