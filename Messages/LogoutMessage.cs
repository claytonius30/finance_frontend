using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Messages
{
    public class LogoutMessage
    {
        public string UserName { get; set; }

        public LogoutMessage()
        {
        }

        public LogoutMessage(string userName)
        {
            UserName = userName;
        }
    }
}
