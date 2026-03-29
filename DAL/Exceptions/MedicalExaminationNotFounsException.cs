using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    public class MedicalExaminationNotFounsException(Guid id):NotFoundException($"MedicalExamination with this id : {id} is not found")
    {
    }
}
