using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Methods
{
    public static class ManageResult
    {
        public static Result result(int changes,string entity,int? id)
        {
            if(changes == 0)
            {
                return Result.Failure($"Fail : {entity} could not be created", new List<string>());
            }
            if(id.HasValue)
            return Result.Success($"Success : {entity} have been created with id {id.Value}", id.Value);

            return Result.Success($"Success : {entity} have been created with id {id.Value}", 0);


        }
    }
}
