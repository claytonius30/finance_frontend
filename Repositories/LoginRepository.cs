using FinanceMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        public Task<LoginModel> Login(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
