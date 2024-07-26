using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Libraries.Command.UpdateCommand
{
    public class UpdateLibraryCommand : IRequest<Result>
    {
        public string libraryId { get; set; }
        public string name { get; set; }


    }

    public class UpdateLibraryCommandHandler : IRequestHandler<UpdateLibraryCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public UpdateLibraryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(UpdateLibraryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Libraries.FindAsync(request.libraryId);
            entity.NameLibrary=request.name;

            _context.Libraries.Update(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0)
            {
                return Result.Success("Library modified with success");
            }
            return Result.Failure("Action Failed : Library could not be modified", new List<string>());
        }
    }
}
