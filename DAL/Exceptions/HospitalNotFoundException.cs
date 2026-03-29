using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    public class HospitalNotFoundException(Guid id):NotFoundException($"Hospital with this id {id} is not found")
    {
    }
}
