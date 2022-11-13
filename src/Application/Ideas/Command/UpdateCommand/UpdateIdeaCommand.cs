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

namespace Application.Ideas.Command.UpdateCommand
{
    public class UpdateIdeaCommand : IRequest<Result>
    {
        public int idIdea { get; set; }
        public string idea { get; set; }
    }

    public class UpdateIdeaCommandHandler : IRequestHandler<UpdateIdeaCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public UpdateIdeaCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(UpdateIdeaCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Ideas
           .FindAsync(new object[] { request.idIdea }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Idees), request.idIdea);
            }


            entity.Idea = request.idea;

            _context.Ideas.Update(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0)
            {
                return Result.Success("Image modified with success");
            }
            return Result.Failure("Action Failed : image could not be modified", new List<string>());
        }
    
    }
}
