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


    public record DeleteItemBasketCommand(int storyTell,string basked_id) : IRequest<Result>;

    public class DeleteItemBasketCommandCommandHandler : IRequestHandler<DeleteItemBasketCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public DeleteItemBasketCommandCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(DeleteItemBasketCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.Basket_items.Where(t=>t.IdStoryTelling == request.storyTell && t.basket_id==request.basked_id).SingleOrDefaultAsync() ?? throw new NotFoundException();
            _context.Basket_items.Remove(item);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);


            if (resultTask > 0) return Result.Success("item deleted with success");
            return Result.Failure("Action Failed : item could not be deleted", new List<string>());
        }
    }
}
