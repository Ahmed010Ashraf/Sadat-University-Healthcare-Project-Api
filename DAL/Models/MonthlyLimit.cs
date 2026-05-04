using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MonthlyLimit
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int Days { get; set; }
    }
}
