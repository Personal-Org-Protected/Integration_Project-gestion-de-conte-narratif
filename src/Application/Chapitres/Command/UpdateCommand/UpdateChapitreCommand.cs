using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Chapitres.Command.UpdateCommand
{
    public record UpdateChapitreCommand(int idChapitre,int idImage):IRequest<Result>;

    public class UpdateChapitreCommandHandler : IRequestHandler<UpdateChapitreCommand, Result>
    {

        private readonly IApplicationDbContext _context;

        public UpdateChapitreCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

         public async Task<Result> Handle(UpdateChapitreCommand request, CancellationToken cancellationToken)
        {
            var chapitre = await  _context.Chapitres.FindAsync(request.idChapitre)
                ?? throw new NotFoundException(nameof(Chapitre), request.idChapitre);

            chapitre.IdImage=request.idImage;
            _context.Chapitres.Update(chapitre);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0)
            {
                return Result.Success("chapitre modified with success");
            }
            return Result.Failure("Action Failed : chapitre could not be modified", new List<string>());

        }
    }
}
