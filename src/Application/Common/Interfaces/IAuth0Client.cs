using Application.Common.Models;
using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IAuth0Client<T> where T:class //where C:class where U : class
    {
        Task<Result> CreateUserAsync(T Enity);
        Task<Result> UpdateUserAsync(T Enity, string user_id);

        Task<Result> DeleteUserAsync(string userId);
        Task<Result> AddressingRole(string roleUser, string RoleId);
        Task<Result> DeleteRoleFromUser(string userId,string role_id);
    }
}
