using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Messages
{
    public class IncomeDeletedMessage
    {
        public int IncomeId { get; set; }
        public Guid UserId { get; set; }
        public string Source { get; set; }

        public IncomeDeletedMessage(string source)
        {
            Source = source;
        }

        public IncomeDeletedMessage(Guid userId, int incomeId, string source)
        {
            IncomeId = incomeId;
            UserId = userId;
            Source = source;
        }
    }
}
