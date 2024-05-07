// Clayton DeSimone
// .NET Applications
// Final Project
// 4/29/2024

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

        public async Task<LockoutInfoModel> GetLockoutEnd(string email)
        {
            using HttpClient client = _httpClientFactory.CreateClient("custom-httpclient");

            try
            {
                LockoutInfoModel? lockoutEnd = await client.GetFromJsonAsync<LockoutInfoModel>(
                    $"api/CustomIdentity/{email}/GetLockoutEnd",
                    new JsonSerializerOptions(JsonSerializerDefaults.Web));

                
                return lockoutEnd!;
        }
            catch (Exception)
            {
                return null;
            }
}

        public async Task<Guid> GetGuid(string email)
        {
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");

            string token = JsonSerializer.Deserialize<LoginResponseModel>(serializedLoginResponseInStorage)!.AccessToken!;
            var httpClient = _httpClientFactory.CreateClient("custom-httpclient");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            //try
            //{
                Guid userId = await httpClient.GetFromJsonAsync<Guid>(
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

        public async Task<bool> PutUser(UserModel model)
        {
            using HttpClient client = _httpClientFactory.CreateClient("custom-httpclient");

            try
            {
                var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"api/User/{model.Id}", content);
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

        public async Task<decimal?> GetCurrentBalance(Guid id)
        {
            using HttpClient client = _httpClientFactory.CreateClient("custom-httpclient");

            try
            {
                decimal balance = await client.GetFromJsonAsync<decimal>(
                    $"api/User/{id}/GetCurrentBalance",
                    new JsonSerializerOptions(JsonSerializerDefaults.Web));

                return balance;
            }
                catch (Exception)
                {
                    return null;
                }
        }

        public async Task<decimal?> GetBalanceForDateRange(Guid id, DateTime startDate, DateTime endDate)
        {
            using HttpClient client = _httpClientFactory.CreateClient("custom-httpclient");

            try
            {
                string url = $"api/User/{id}/GetBalanceForDateRange" +
                             $"?startDate={startDate:s}&endDate={endDate:s}";

                decimal balance = await client.GetFromJsonAsync<decimal>(url);
                
                return balance;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<TransactionModel>> GetAllTransactions(Guid userId)
        {
            using HttpClient client = _httpClientFactory.CreateClient("custom-httpclient");

            try
            {
                List<TransactionModel>? transactions = await client.GetFromJsonAsync<List<TransactionModel>>(
                    $"api/User/{userId}/GetAllTransactions",
                    new JsonSerializerOptions(JsonSerializerDefaults.Web));

                return transactions ?? new List<TransactionModel>();
            }
            catch (Exception)
            {
                return new List<TransactionModel>();
            }
        }

        public async Task<List<TransactionModel>> GetTransactionsForDateRange(Guid userId, DateTime startDate, DateTime endDate)
        {
            using HttpClient client = _httpClientFactory.CreateClient("custom-httpclient");

            try
            {
                string url = $"api/User/{userId}/GetTransactionsForDateRange" +
                             $"?startDate={startDate:s}&endDate={endDate:s}";

                List<TransactionModel>? transactions = await client.GetFromJsonAsync<List<TransactionModel>>(url);
                
                return transactions ?? new List<TransactionModel>();
            }
            catch (Exception)
            {
                return new List<TransactionModel>();
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

        public async Task<List<IncomeModel>> GetIncomesForDateRange(Guid userId, DateTime startDate, DateTime endDate)
        {
            using HttpClient client = _httpClientFactory.CreateClient("custom-httpclient");

            try
            {
                string url = $"api/User/{userId}/GetIncomesForDateRange" +
                             $"?startDate={startDate:s}&endDate={endDate:s}";

                List<IncomeModel>? incomes = await client.GetFromJsonAsync<List<IncomeModel>>(url);
                
                return incomes ?? new List<IncomeModel>();
            }
            catch (Exception)
            {
                return new List<IncomeModel>();
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

        public async Task<List<ExpenseModel>> GetExpenses(Guid userId)
        {
            using HttpClient client = _httpClientFactory.CreateClient("custom-httpclient");

            try
            {
                List<ExpenseModel>? expenses = await client.GetFromJsonAsync<List<ExpenseModel>>(
                    $"api/User/{userId}/GetExpenses",
                    new JsonSerializerOptions(JsonSerializerDefaults.Web));

                return expenses ?? new List<ExpenseModel>();
            }
            catch (Exception)
            {
                return new List<ExpenseModel>();
            }
        }

        public async Task<List<ExpenseModel>> GetExpensesForDateRange(Guid userId, DateTime startDate, DateTime endDate)
        {
            using HttpClient client = _httpClientFactory.CreateClient("custom-httpclient");

            try
            {
                string url = $"api/User/{userId}/GetExpensesForDateRange" +
                             $"?startDate={startDate:s}&endDate={endDate:s}";

                List<ExpenseModel>? expenses = await client.GetFromJsonAsync<List<ExpenseModel>>(url);

                return expenses ?? new List<ExpenseModel>();
            }
            catch (Exception)
            {
                return new List<ExpenseModel>();
            }
        }

        public async Task<ExpenseModel?> GetExpense(Guid userId, int expenseId)
        {
            using HttpClient client = _httpClientFactory.CreateClient("custom-httpclient");

            try
            {
                ExpenseModel? expense = await client.GetFromJsonAsync<ExpenseModel>(
                    $"api/User/{userId}/GetExpense/{expenseId}",
                    new JsonSerializerOptions(JsonSerializerDefaults.Web));

                return expense;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> CreateExpense(ExpenseModel model)
        {
            using HttpClient client = _httpClientFactory.CreateClient("custom-httpclient");

            try
            {
                var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"api/User/{model.Id}/AddExpense", content);
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

        public async Task<bool> EditExpense(ExpenseModel model)
        {
            using HttpClient client = _httpClientFactory.CreateClient("custom-httpclient");

            try
            {
                var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"api/User/{model.Id}/UpdateExpense/{model.ExpenseId}", content);
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

        public async Task<bool> DeleteExpense(Guid userId, int expenseId)
        {
            using HttpClient client = _httpClientFactory.CreateClient("custom-httpclient");

            try
            {
                var response = await client.DeleteAsync($"api/User/{userId}/DeleteExpense/{expenseId}");
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

        public async Task<List<GoalModel>> GetGoals(Guid userId)
        {
            using HttpClient client = _httpClientFactory.CreateClient("custom-httpclient");

            try
            {
                List<GoalModel>? goals = await client.GetFromJsonAsync<List<GoalModel>>(
                    $"api/User/{userId}/GetGoals",
                    new JsonSerializerOptions(JsonSerializerDefaults.Web));

                return goals ?? new List<GoalModel>();
            }
            catch (Exception)
            {
                return new List<GoalModel>();
            }
        }

        public async Task<GoalModel?> GetGoal(Guid userId, int goalId)
        {
            using HttpClient client = _httpClientFactory.CreateClient("custom-httpclient");

            try
            {
                GoalModel? goal = await client.GetFromJsonAsync<GoalModel>(
                    $"api/User/{userId}/GetGoal/{goalId}",
                    new JsonSerializerOptions(JsonSerializerDefaults.Web));

                return goal;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> CreateGoal(GoalModel model)
        {
            using HttpClient client = _httpClientFactory.CreateClient("custom-httpclient");

            try
            {
                var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"api/User/{model.Id}/AddGoal", content);
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

        public async Task<bool> EditGoal(GoalModel model)
        {
            using HttpClient client = _httpClientFactory.CreateClient("custom-httpclient");

            try
            {
                var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"api/User/{model.Id}/UpdateGoal/{model.GoalId}", content);
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

        public async Task<bool> DeleteGoal(Guid userId, int goalId)
        {
            using HttpClient client = _httpClientFactory.CreateClient("custom-httpclient");

            try
            {
                var response = await client.DeleteAsync($"api/User/{userId}/DeleteGoal/{goalId}");
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
