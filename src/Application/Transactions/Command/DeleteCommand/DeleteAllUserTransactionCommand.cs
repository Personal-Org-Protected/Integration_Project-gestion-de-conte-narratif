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
    public record DeleteAllUserTransactionCommand():IRequest<Result>;

    public class DeleteAllUserTransactionCommandHandler : IRequestHandler<DeleteAllUserTransactionCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;

        public DeleteAllUserTransactionCommandHandler(IApplicationDbContext context,IUser user)
        {
            _context = context;
            _user = user;

        }
        public async Task<Result> Handle(DeleteAllUserTransactionCommand request, CancellationToken cancellationToken)
        {
            var user_id=_user.getUserId();
            var transactions = await _context.Transactions
                .Where(t => t.user_id ==user_id)
                .ToListAsync() ?? throw new NotFoundException();
            _context.Transactions.RemoveRange(transactions);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);


            if (resultTask > 0) return Result.Success("Commentary deleted with success");
            return Result.Failure("Action Failed : Commentary could not be deleted", new List<string>());
        }
    }
}
