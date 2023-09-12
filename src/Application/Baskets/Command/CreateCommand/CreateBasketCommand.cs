using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Baskets.Command.CreateCommand
{
    public class CreateBasketCommand : IRequest<Result>
    {
        public string user_id { get; set; }

    }

    public class CreateBasketCommandCommandHandler : IRequestHandler<CreateBasketCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public CreateBasketCommandCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            var entity = new Basket()
            {
                basket_id = "Basket_" + request.user_id,
                isEmpty = true,
                user_id = request.user_id,

            };
            await _context.Basket.AddAsync(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0)
            {
                return Result.Success("Basket added with success");
            }
            return Result.Failure("Action Failed : Basket could not be added", new List<string>());
        }
    }


}
