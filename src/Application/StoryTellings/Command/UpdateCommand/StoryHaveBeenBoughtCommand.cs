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
    public record StoryHaveBeenBoughtCommand(int storyTell):IRequest<Result>;
  
    public class StoryHaveBeenBoughtCommandHandler : IRequestHandler<StoryHaveBeenBoughtCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public StoryHaveBeenBoughtCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(StoryHaveBeenBoughtCommand request, CancellationToken cancellationToken)
        {
            var histoire = await _context.StoryTellings
                .FindAsync(request.storyTell)
                ?? throw new NotFoundException();

            histoire.numberRef++;
            _context.StoryTellings.Update(histoire);
            var result=await _context.SaveChangesAsync(cancellationToken);
            if (result > 0) return Result.Success("Book bought one more time");
            return Result.Failure("Book count buy is not updated", new List<string>());
        }
    }
}
