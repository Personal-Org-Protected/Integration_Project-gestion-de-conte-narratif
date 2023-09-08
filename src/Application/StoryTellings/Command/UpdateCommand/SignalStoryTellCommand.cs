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
    public record SignalStoryTellCommand() :IRequest<Result>;
    public class SignalStoryTellCommandHandler : IRequestHandler<SignalStoryTellCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public SignalStoryTellCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public Task<Result> Handle(SignalStoryTellCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
