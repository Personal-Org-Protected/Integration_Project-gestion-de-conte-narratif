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

namespace Application.Stories.Command.UpdateCommand
{
    public class UpdateStoriesCommand :IRequest<Result>
    {
        public int IdStory { get; set; }
        public string NomStory { get; set; }
        public string TextStory { get; set; }
    }

    public class UpdateStoriesCommandHandler : IRequestHandler<UpdateStoriesCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public UpdateStoriesCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(UpdateStoriesCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Stories
          .FindAsync(new object[] { request.IdStory }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Story), request.IdStory);
            }

            entity.NomStory = request.NomStory;
            entity.TextStory = request.TextStory;
            entity.DateModif=DateTime.Now;

            _context.Stories.Update(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0) return Result.Success("Story modified with success");
            return Result.Failure("Action Failed : Story could not be modified", new List<string>());
        }
    }
}
