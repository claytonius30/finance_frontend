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
    public class TransactionModel
    {
        public string Type { get; set; } = default!;
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = default!;
        public DateTime Date { get; set; }
    }
}
