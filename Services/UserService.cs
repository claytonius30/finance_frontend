using FinanceMAUI.Models;
using FinanceMAUI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<UserModel?> GetUser(int id)
            => _userRepository.GetUser(id);

        public Task<double> GetCurrentBalance(int id)
            => _userRepository.GetCurrentBalance(id);
    }
}
