using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Methods
{
    public  class UserInfo: IUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _type = @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        private readonly string _user_prefix = "auth0|";
        public UserInfo(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string getUserId()
        {
            return process();
        }

       private bool checkAudience()
        {
           var audience= _httpContextAccessor.HttpContext.Request.Headers.SingleOrDefault(t => t.Key == "audience");
            if (audience.Value!="Flutter") return false;

            return true;
        }

        private string process()
        {
            var user_id = "";
            if (checkAudience()) { user_id = "auth0|UserLambda_270b7c19-1968-4920-970a-e3deed612cb3";/*_httpContextAccessor.HttpContext.Request.Headers.SingleOrDefault(t => t.Key == "user").Value;*/ }
            else
            {
                var claims = _httpContextAccessor.HttpContext.User.Claims ?? throw new ForbiddenAccessException("no user found");
                var userInfo = claims.SingleOrDefault(t => t.Type == _type);
                user_id = userInfo.Value.Replace(_user_prefix, "");
            }
            user_id = user_id.Replace(_user_prefix, "");
            return user_id;

        }
    }
}
