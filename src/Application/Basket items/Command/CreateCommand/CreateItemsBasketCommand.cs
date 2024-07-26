using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Basket_items.Command.CreateCommand
{
    public class CreateItemsBasketCommand : IRequest<Result>
    {
        public int itemId { get; set; }

    }

    public class CreateItemsBasketCommandHandler : IRequestHandler<CreateItemsBasketCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;

        public CreateItemsBasketCommandHandler(IApplicationDbContext context, IUser user)
        {
            _context = context;
            _user = user;
        }
        public async Task<Result> Handle(CreateItemsBasketCommand request, CancellationToken cancellationToken)
        {
            var user_id = _user.getUserId();
            var entity = new BasketItems()
            {
                basket_id = "Basket_" + user_id,
                IdStoryTelling = request.itemId,

            };
            _context.Basket_items.Add(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0)
            {
                return Result.Success("item added with success");
            }
            return Result.Failure("Action Failed : item could not be added", new List<string>());
        }
    }
}
