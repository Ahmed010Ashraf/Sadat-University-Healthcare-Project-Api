using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    public class RoleNotFoundException(string name):NotFoundException($"role with this name :{name} is not found")
    {
    }
}
