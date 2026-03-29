using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    public class MedicalExaminationRequestNotFoundException(Guid id):NotFoundException($"the medical examination request with this id :{id} is not found")
    {
    }
}
