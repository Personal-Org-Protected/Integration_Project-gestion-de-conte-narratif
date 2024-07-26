using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StoryTellings.Command.UpdateCommand
{
    public record OngoingStoryCommand(int storyTell) : IRequest<Result>;

    public class OngoingStoryCommandHandler : IRequestHandler<OngoingStoryCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public OngoingStoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(OngoingStoryCommand request, CancellationToken cancellationToken)
        {
            var histoire = await _context.StoryTellings
                .FindAsync(request.storyTell) ?? throw new NotFoundException();
            histoire.Finished = false;
            var result = await _context.SaveChangesAsync(cancellationToken);
            if (result > 0) return Result.Success("Book is now Available in store");
            return Result.Failure("Book could be added to the store", new List<string>());

        }
    }
}
