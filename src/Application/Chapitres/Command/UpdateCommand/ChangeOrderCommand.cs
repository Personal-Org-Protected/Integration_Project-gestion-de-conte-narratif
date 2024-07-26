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

namespace Application.Chapitres.Command.UpdateCommand
{

    public record ChangeOrderCommand(int idStory,int idChapitre, int order) : IRequest<Result>;
    public class ChangeOrderCommandHandler : IRequestHandler<ChangeOrderCommand, Result>
    {

        private readonly IApplicationDbContext _context;

        public ChangeOrderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(ChangeOrderCommand request, CancellationToken cancellationToken)
        {
            var formerChapter=await _context.Chapitres
                .SingleOrDefaultAsync(t=>t.IdStoryTelling==request.idStory && t.Order==request.order) ?? throw new NotFoundException(nameof(Chapitre), request.idChapitre);

            var chapitre = await _context.Chapitres.FindAsync(request.idChapitre)
                ?? throw new NotFoundException(nameof(Chapitre), request.idChapitre);
            formerChapter.Order = chapitre.Order;
            chapitre.Order = request.order;
            _context.Chapitres.UpdateRange(new List<Chapitre>() { chapitre, formerChapter });
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0)
            {
                return Result.Success("chapitre modified with success");
            }
            return Result.Failure("Action Failed : chapitre could not be modified", new List<string>());

        }
    }
}
