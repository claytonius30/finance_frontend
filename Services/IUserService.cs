using FinanceMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Services
{
    public interface IUserService
    {
        Task<UserModel?> GetUser(int id);

        Task<double> GetCurrentBalance(int id);
    }
}
