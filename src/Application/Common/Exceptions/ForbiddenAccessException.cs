using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException(string message):base(message)
        {

        }
        public ForbiddenAccessException(string CurrentUser,string legitimateUser)
         : base($"the user {CurrentUser} is not allowed to access this action, only the users of status : {legitimateUser}")
        {
        }
    }
}
