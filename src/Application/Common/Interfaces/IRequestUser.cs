using Application.Common.Models;
using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IRequestUser<T>where T : class
    {

        Task<IList<T>> GetAsync();
        Task<T> GetByIdAsync(string user_id);
        Task<Result> CreateAsync(User user);
        Task<Result> UpdateAsync(User user);
        Task<Result> DeleteAsync(string idUser);

        Task<Result> ResultResponse(Result resultAuth0, int resultDbContext);
        
    }
}
