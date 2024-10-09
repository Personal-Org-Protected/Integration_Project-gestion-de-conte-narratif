using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Command.CreateCommand
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateUserCommandValidator(IApplicationDbContext context)
        {//adapte a auth rule
            _context = context;
            RuleFor(v => v.user_id)
                .NotNull()
                .NotEmpty().WithMessage("user id required")
                .MustAsync((x, cancellationToken) => UserIdExist(x, cancellationToken)).WithMessage("user already exist");
        }

        private async Task<bool> UserIdExist(string username, CancellationToken cancellationToken)
        {
            return ! await _context.Users
                .AnyAsync(t=> t.user_id == username, cancellationToken);
        }
        //private async Task<bool> PhoneNumberdExist(string phoneNumber, CancellationToken cancellationToken)
        //{
        //    return ! await _context.Users
        //        .AnyAsync(t => t.phoneNumber == phoneNumber, cancellationToken);
        //}
    }
}
