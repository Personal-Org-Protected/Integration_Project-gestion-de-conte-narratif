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
        public string Owner { get; set; }
        public string Commentaire { get; set; }
        public int IdZone { get; set; }
    }

    public class CreateCommentaryCommandHandler : IRequestHandler<CreateCommentaryCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public CreateCommentaryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(CreateCommentaryCommand request, CancellationToken cancellationToken)
        {
            var userEntity = await _context.userEntities
                .Where(t => t.user_id == request.Owner)
                .SingleOrDefaultAsync();
            var entity = new Commentaires
            {
                Commentaire = request.Commentaire,
                Owner = userEntity.IdUser,
                IdZone =request.IdZone,
                DateCreation=DateTime.Now,
                signal=10
            };

            _context.Commentaries.Add(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0) return Result.Success("Commentary added with success");
            return Result.Failure("Action Failed : Commentary could not be added", new List<string>());
        }
    }
}
