using FinanceMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel?> GetUser(int id);

        Task<double> GetCurrentBalance(int id);
    }
}
