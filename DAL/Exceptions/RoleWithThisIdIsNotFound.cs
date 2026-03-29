using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    public class RoleWithThisIdIsNotFound(Guid id):NotFoundException($"role with this id :{id} is not found")
    {
    }
}
