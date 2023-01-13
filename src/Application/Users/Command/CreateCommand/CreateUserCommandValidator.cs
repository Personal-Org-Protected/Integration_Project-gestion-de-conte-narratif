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
            RuleFor(v => v.Location)
                .NotNull()
                .NotEmpty().WithMessage("A location must be given");
            RuleFor(v => v.username)
                .NotNull()
                .NotEmpty().WithMessage("Username required")
                .MaximumLength(15).WithMessage("too much character")
                .MustAsync((x, cancellationToken) => UserIdExist(x, cancellationToken)).WithMessage("username already taken "); 
            RuleFor(v => v.BirthDate)
                .NotNull()
               .NotEmpty().WithMessage("Birthdate required")
               .MustAsync((x, cancellation) => isMajeur(x)).WithMessage("ce user n'est pas majeur");
            RuleFor(v => v.email)
                .NotNull()
               .NotEmpty().WithMessage("email required")
               .MustAsync((x, cancellationToken) => EmailExist(x,cancellationToken)).WithMessage("email existe");
            RuleFor(v => v.password)
               .NotNull()
               .NotEmpty().WithMessage("password is necessarry")
               .MaximumLength(15).WithMessage("too long")
               .MinimumLength(5).WithMessage("need more than that as password")
               .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$")
               .WithMessage("Lower case Upper case and number needed");
            RuleFor(v=>v.phoneNumber)
                .Length(10).WithMessage("must be 10 character")
                .MustAsync((x, cancellationToken) => PhoneNumberdExist(x, cancellationToken)).WithMessage("phone already taken");

            RuleFor(v => v.description)
         .MaximumLength(200).WithMessage("must be max 200  character");

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
        private async Task<bool> PhoneNumberdExist(string phoneNumber, CancellationToken cancellationToken)
        {
            return ! await _context.Users
                .AnyAsync(t => t.phoneNumber == phoneNumber, cancellationToken);
        }
    }
}
