using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StoryTellings.Command.UpdateCommand
{
    public class StopReadingCommandValidator : AbstractValidator<StopReadingBookCommand>
    {
        private readonly IApplicationDbContext _context;
        public StopReadingCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.order)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("pg number not valide");
        }



    }
}
