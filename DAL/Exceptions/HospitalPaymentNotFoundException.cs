using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    public class HospitalPaymentNotFoundException(Guid id):NotFoundException($"HospitalPayment with this id {id} is not found")
    {
    }
}
