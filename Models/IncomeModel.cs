using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Models
{
    public class IncomeModel
    {
        public int IncomeId { get; set; }
        public string Source { get; set; } = default!;
        public decimal Amount { get; set; }
        public DateTime DateReceived { get; set; }
        public int Id { get; set; }
    }
}
