using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos.errors
{
    public class ValidationErrors
    {
        public List<Errors> Errors { get; set; } 
        public string message { get; set; }

        public int StatusCode { get; set; } 
    }

   public class Errors
    {
        public string Key { get; set; }

        public List<string> ErrorMessages { get; set; }  
    }
}
