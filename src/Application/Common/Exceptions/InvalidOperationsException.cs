using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class InvalidOperationsException : Exception
    {
        public InvalidOperationsException()
          : base("this operations is not valid")
        {
        }

        public InvalidOperationsException(string message)
            : base(message)
        {
        }

        public InvalidOperationsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public InvalidOperationsException(string msg, object method)
            : base($"this operations is not valid: {method} " + msg)
        {
        }
    }
}
