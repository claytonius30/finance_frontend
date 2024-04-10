using FinanceMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Repositories
{
    internal interface ILoginRepository
    {
        Task<LoginModel> Login(string userName, string password);
    }
}
