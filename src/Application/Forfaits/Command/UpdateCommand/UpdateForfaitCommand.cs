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

namespace Application.Forfaits.Command.UpdateCommand
{
    public class UpdateForfaitCommand : IRequest<Result>
    {
        public int IdForfait { get; set; }
        public string ForfaitLibelle { get; set; }
        public double ForfaitValue { get; set; }
    }
    public class UpdateForfaitCommandHandler : IRequestHandler<UpdateForfaitCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public UpdateForfaitCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(UpdateForfaitCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Forfaits
            .FindAsync(request.IdForfait ) ?? throw new NotFoundException(nameof(ForfaitClient), request.IdForfait); ;

            entity.ForfaitLibelle = request.ForfaitLibelle;
            entity.ForfaitValue = request.ForfaitValue;

            _context.Forfaits.Update(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0)
            {
                return Result.Success("Forfait modified with success");
            }
            return Result.Failure("Action Failed : Forfait could not be modified", new List<string>());
        }
    
    }
}
