using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StoryBox.Command.UpdateCommand
{
    public record StopReadingBookCommand(int idBox, int order) : IRequest<Result>;


    public class StopReadingBookCommandHandler : IRequestHandler<StopReadingBookCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public StopReadingBookCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(StopReadingBookCommand request, CancellationToken cancellationToken)
        {
            var box = await _context.storyTellBoxes.FindAsync(request.idBox)
                ?? throw new NotFoundException("Box Not Found");

            box.lastPageChecked = request.order;
            _context.storyTellBoxes.Update(box);
            var result = await _context.SaveChangesAsync(cancellationToken);

            if (result > 0) return Result.Success("Box closed with success");
            return Result.Failure("Box could not be closed correctly", new List<string>());

        }
    }
}
