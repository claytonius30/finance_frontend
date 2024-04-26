using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Models
{
    public class ExpenseModel
    {
        public int ExpenseId { get; set; }
        public string Category { get; set; } = default!;
        public decimal Amount { get; set; }
        public DateTime DateIncurred { get; set; }
        public Guid Id { get; set; }
    }
}
