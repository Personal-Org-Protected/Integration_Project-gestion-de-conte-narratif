using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{

    public class Result
    {

        public Result(bool succeeded,string message, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Msg= message;
            Errors = errors.ToArray();
        }
        public Result(bool succeeded, string message, IEnumerable<string> errors,int id)
        {
            idEntity = id;
            Succeeded = succeeded;
            Msg = message;
            Errors = errors.ToArray();
        }
        public int idEntity { get; set; }
        public bool Succeeded { get; set; }
        public string Msg { get; set; }
        public string[] Errors { get; set; }

        public static Result Success(string message)
        {
            return new Result(true,message, new string[] { });
        }

        public static Result Success(string message,int id)
        {
            return new Result(true, message, new string[] { },id);
        }
        public static Result Failure(string message,IEnumerable<string> errors)
        {
            return new Result(false,message, errors);
        }
    }
}
