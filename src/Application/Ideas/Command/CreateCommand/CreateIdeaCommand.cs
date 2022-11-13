using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Ideas.Command.CreateCommand
{
    public class CreateIdeaCommand : IRequest<Result>
    {
        public string idea { get; set; }
        public int StoryId { get; set; }
    }

    public class CreateIdeaCommandHandler : IRequestHandler<CreateIdeaCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public CreateIdeaCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(CreateIdeaCommand request, CancellationToken cancellationToken)
        {
            var entity = new Idees()
            {
                Idea = request.idea,
                IdStoryTelling = request.StoryId
                
            };
            _context.Ideas.Add(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0)
            {
                return Result.Success("Ideas added with success");
            }
            return Result.Failure("Action Failed : Ideas could not be added", new List<string>());
        }
    
    }
}
