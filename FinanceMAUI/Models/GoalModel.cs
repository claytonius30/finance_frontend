﻿// Clayton DeSimone
// .NET Applications
// Final Project
// 4/29/2024

using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Models
{
    public class GoalModel
    {
        public int GoalId { get; set; }
        public DateTime SetDate { get; set; }
        public DateTime GoalDate {  get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = default!;
        public string Status { get; set; } = default!;
        public Guid Id { get; set; }
    }
}
