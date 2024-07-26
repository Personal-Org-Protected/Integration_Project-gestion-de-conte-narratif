using Application.Common.Exceptions;
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

namespace Application.Commentaries.Command.CreateCommand
{
    public class CreateCommentaryCommand : IRequest<Result>
    {
        public string Commentaire { get; set; }
        public int IdZone { get; set; }
    }

    public class CreateCommentaryCommandHandler : IRequestHandler<CreateCommentaryCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;

        public CreateCommentaryCommandHandler(IApplicationDbContext context,IUser user)
        {
            _context = context;
            _user = user;
        }
        public async Task<Result> Handle(CreateCommentaryCommand request, CancellationToken cancellationToken)
        {
            var user_id = _user.getUserId();
            var entity = new Commentaires
            {
                Commentaire = request.Commentaire,
                user_id =user_id,
                IdZone =request.IdZone,
                DateCreation=DateTime.Now,
                signal=0
            };

            await _context.Commentaries.AddAsync(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0) return Result.Success("Commentary added with success");
            return Result.Failure("Action Failed : Commentary could not be added", new List<string>());
        }
    }
}
