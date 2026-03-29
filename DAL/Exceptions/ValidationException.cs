using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    public class ValidationException:Exception
    {
        public IEnumerable<string> Errors { get; set; }

        public ValidationException(IEnumerable<string> _Errors):base("Validation Errors")
        {
            Errors = _Errors;
        }
    }
}
