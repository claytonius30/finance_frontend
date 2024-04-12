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

        public IncomeDeletedMessage(Guid userId, int incomeId)
        {
            IncomeId = incomeId;
            UserId = userId;
        }
    }
}
