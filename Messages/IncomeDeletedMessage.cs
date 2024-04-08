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
        public int UserId { get; set; }

        public IncomeDeletedMessage(int incomeId, int userId)
        {
            IncomeId = incomeId;
            UserId = userId;
        }
    }
}
