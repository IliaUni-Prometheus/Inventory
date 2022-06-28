using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }

    public class ErrorDetails
    {
        public int? Status { get; set; }
        public Error Error { get; set; }
    }
}
