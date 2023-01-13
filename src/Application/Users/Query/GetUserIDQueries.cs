using Application.Users.Query.Dto_s;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Query
{
    public record GetUserIDQueries(string username):IRequest<UserIdDto>;

    public class GetUserIDQueriesHandler : IRequestHandler<GetUserIDQueries, UserIdDto>
    {
        public async Task<UserIdDto> Handle(GetUserIDQueries request, CancellationToken cancellationToken)
        {
            return new UserIdDto()
            {
               user_id= request.username + "_" + Guid.NewGuid()
            };
        }
    }
}
