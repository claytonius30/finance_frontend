﻿// Clayton DeSimone
// .NET Applications
// Final Project
// 4/29/2024

using FinanceMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Services
{
    public interface IClientService
    {
        Task Register(RegisterModel model);
        
        Task Login(LoginModel model);
    }
}
