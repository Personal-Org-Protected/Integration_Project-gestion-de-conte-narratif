using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Command.UpdateCommand
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateUserCommandValidator(IApplicationDbContext context)
        {
            _context = context;
            RuleFor(v => v.Location)
                .NotEmpty().WithMessage("A location must be given");
            //    .MustAsync((x, cancellationToken) => UserIdExist(x, cancellationToken)).WithMessage("username already taken ");
            //RuleFor(v => v.phoneNumber)
            //    .NotEmpty()
            //    .MustAsync((x, cancellationToken) => PhoneNumberdExist(x, cancellationToken)).WithMessage("phone already taken ");

        }

        //private async Task<bool> UserIdExist(string username, CancellationToken cancellationToken)
        //{
        //    var entity=await _context.Users.Where(t=>t.username == username).FirstOrDefaultAsync();
        //    return await _context.Users
        //        .AllAsync(t => t.username != username, cancellationToken);
        //}
        //private async Task<bool> PhoneNumberdExist(string phoneNumber, CancellationToken cancellationToken)
        //{
        //    return await _context.Users
        //        .AllAsync(t => t.phoneNumber != phoneNumber, cancellationToken);
        //}
    }
}
