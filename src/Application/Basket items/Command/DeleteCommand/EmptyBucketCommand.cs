using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Basket_items.Command.DeleteCommand
{
    public record EmptyBucketCommand(string basked_id) : IRequest<Result>;

    public class EmptyBucketCommandCommandHandler : IRequestHandler<EmptyBucketCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public EmptyBucketCommandCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(EmptyBucketCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.Basket_items
                .Where(t => t.basket_id == request.basked_id)
                .ToListAsync() ?? throw new NotFoundException();
            _context.Basket_items.RemoveRange(item);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);


            if (resultTask > 0) return Result.Success("item deleted with success");
            return Result.Failure("Action Failed : item could not be deleted", new List<string>());
        }
    }
}
