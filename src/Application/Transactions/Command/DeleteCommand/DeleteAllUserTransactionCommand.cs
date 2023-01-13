using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Transactions.Command.DeleteCommand
{
    public record DeleteAllUserTransactionCommand(string user_id):IRequest<Result>;

    public class DeleteAllUserTransactionCommandHandler : IRequestHandler<DeleteAllUserTransactionCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public DeleteAllUserTransactionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(DeleteAllUserTransactionCommand request, CancellationToken cancellationToken)
        {
            var transactions = await _context.Transactions
                .Where(t => t.user_id ==request.user_id)
                .ToListAsync() ?? throw new NotFoundException();
            _context.Transactions.RemoveRange(transactions);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);


            if (resultTask > 0) return Result.Success("Commentary deleted with success");
            return Result.Failure("Action Failed : Commentary could not be deleted", new List<string>());
        }
    }
}
