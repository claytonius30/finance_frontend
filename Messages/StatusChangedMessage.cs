using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Messages
{
    public class StatusChangedMessage
    {
        public Guid Id { get; set; }
        public string Status { get; set; }

        public StatusChangedMessage(Guid id, string status)
        {
            Id = id;
            Status = status;
        }
    }
}
