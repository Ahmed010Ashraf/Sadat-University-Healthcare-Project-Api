using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Enums
{
    public enum MedicalExaminationRequestStatus
    {
        Pending = 1,
        AutoApproved = 2,
        Approved = 3,
        Rejected = 4,
    }
}
