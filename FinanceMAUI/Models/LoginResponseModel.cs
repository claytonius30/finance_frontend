// Clayton DeSimone
// .NET Applications
// Final Project
// 4/29/2024

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Models
{
    public class LoginResponseModel
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public string? UserName { get; set; }
    }
}
