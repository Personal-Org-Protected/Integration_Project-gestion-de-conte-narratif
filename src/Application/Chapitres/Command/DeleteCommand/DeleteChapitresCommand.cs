using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Chapitres.Command.DeleteCommand
{
    public record DeleteChapitresCommand(int id):IRequest<Result>;


    public class DeleteChapitresCommandHandler : IRequestHandler<DeleteChapitresCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public DeleteChapitresCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(DeleteChapitresCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Chapitres
           .Where(l => l.IdChapitre == request.id)
           .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Images), request.id);
            }
            _context.Chapitres.Remove(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            //var resultStory=await deleteStory(entity.IdStory,cancellationToken);

            if (resultTask > 0) return Result.Success("Chapitre deleted with success");
            return Result.Failure("Action Failed : Chapitre could not be deleted", new List<string>()); //delete story avec
        }

        //private async Task<Result> deleteStory(int idStory,CancellationToken cancellationToken)
        //{
        //   var story=await _context.Stories.FindAsync(idStory)??throw new NotFoundException();
        //    _context.Stories.Remove(story);
        //    var resultTask = await _context.SaveChangesAsync(cancellationToken);


        //    if (resultTask > 0) return Result.Success("Chapitre deleted with success");
        //    return Result.Failure("Action Failed : Chapitre could not be deleted", new List<string>()); //delete story avec
        //}
    }
}
