using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Forfaits.Command.CreateCommand
{
    public class CreateForfaitCommand : IRequest<Result>
    {//role 
        public string ForfaitLibelle { get; set; }
        public double ForfaitValue { get; set; }
        public double reduction { get; set; }
        public bool IsForAuthor { get; set; }
        public int IdRole { get; set; }
    }

    public class CreateForfaitCommandHandler : IRequestHandler<CreateForfaitCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public CreateForfaitCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(CreateForfaitCommand request, CancellationToken cancellationToken)
        {
            var entity = new ForfaitClient
            {
                ForfaitLibelle = request.ForfaitLibelle,
                ForfaitValue = request.ForfaitValue,
                RoleId = request.IdRole,
                Reduction=request.reduction,
                IsForAuthor= request.IsForAuthor
                
            };

            await _context.Forfaits.AddAsync(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0) return Result.Success("Forfait added with success");
            return Result.Failure("Action Failed : Forfait could not be added", new List<string>());
        }
    }
}
