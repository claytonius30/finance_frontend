using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceMAUI.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? UserName { get; set; }
        public double? Balance { get; set; }
        public DateTime? Date { get; set; }
    }
}
