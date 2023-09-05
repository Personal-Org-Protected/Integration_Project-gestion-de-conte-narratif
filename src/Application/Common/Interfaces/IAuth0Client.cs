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
        Task CreateUserAsync(T Enity);
        Task UpdateUserAsync(T Enity, string user_id);

        Task DeleteUserAsync(string userId);
        Task AddressingRole(string roleUser, string RoleId);
        Task DeleteRoleFromUser(string userId,string role_id);
    }
}
