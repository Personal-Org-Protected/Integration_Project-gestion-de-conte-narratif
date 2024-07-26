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
            RuleFor(v => v.username)
                .NotNull()
                .NotEmpty().WithMessage("Username required")
                .MaximumLength(15).WithMessage("too much character")
                .MustAsync((x, cancellationToken) => UserIdExist(x, cancellationToken)).WithMessage("username already taken ");
            RuleFor(v => v.email)
              .NotNull()
              .NotEmpty().WithMessage("email required")
              .MaximumLength(15).WithMessage("too much character")
              .MustAsync((x, cancellationToken) => EmailExist(x, cancellationToken)).WithMessage("email already taken ");
            RuleFor(v => v.userRole)
              .NotNull()
              .NotEmpty().WithMessage("userRole required")
              .Must((x)=>IsStandardRole(x)).WithMessage("the role can only be member or admin");


        }

        private bool IsStandardRole(string role)
        {
            if (role != "member" || role!= "admin") return false;
            return true;
        }
        private async Task<bool> EmailExist(string email,CancellationToken cancellationToken)
        {
            return ! await _context.Users
                .AnyAsync(t => t.email == email, cancellationToken);
        }
        private async Task<bool> UserIdExist(string username, CancellationToken cancellationToken)
        {
            return ! await _context.Users
                .AnyAsync(t=> t.username == username, cancellationToken);
        }
        private async Task<bool> isMajeur(DateTime birth)
        {
            return DateTime.Now.Year - 18 >= birth.Year;
        }
        //private async Task<bool> PhoneNumberdExist(string phoneNumber, CancellationToken cancellationToken)
        //{
        //    return ! await _context.Users
        //        .AnyAsync(t => t.phoneNumber == phoneNumber, cancellationToken);
        //}
    }
}
