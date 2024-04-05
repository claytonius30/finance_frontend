using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Messages
{
    public class StatusChangedMessage
    {
        public int Id { get; set; }
        public string Status { get; set; }

        public StatusChangedMessage(int id, string status)
        {
            Id = id;
            Status = status;
        }
    }
}
