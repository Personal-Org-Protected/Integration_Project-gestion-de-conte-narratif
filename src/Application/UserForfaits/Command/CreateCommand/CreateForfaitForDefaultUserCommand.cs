using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserForfaits.Command.CreateCommand
{
    public record CreateForfaitForDefaultUserCommand(string user_id) :IRequest<Result>;

    public class CreateForfaitForDefaultUserCommandHandler : IRequestHandler<CreateForfaitForDefaultUserCommand,Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly int idRole = 2;
        private readonly string ForfaitName = "Default";

        public CreateForfaitForDefaultUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(CreateForfaitForDefaultUserCommand request, CancellationToken cancellationToken)
        {
            var forfait = await _context.Forfaits
                      .Where(t => t.RoleId == idRole && t.ForfaitLibelle == ForfaitName)
                      .FirstOrDefaultAsync();
            var ForfaitUser = new Forfait_UserIntern()
            {
                IdForfait = forfait.IdForfait,
                user_id = request.user_id,
            };
            _context.Forfait_Users.Add(ForfaitUser);
            var result= await _context.SaveChangesAsync(cancellationToken);

            if(result>0)return Result.Success("Forfait added for the user : "+ request.user_id);

            return Result.Failure("Forfait could not be added for this user", new List<string>() { "user may already have the forfait" });
        }
    }
}
