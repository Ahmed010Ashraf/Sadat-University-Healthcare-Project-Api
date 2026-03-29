using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Enums
{
    public enum PrescriptionStatus
    {
        Active = 1,
        FullyDispensed = 2,
        Expired = 3,
        Cancelled = 4
    }
}
