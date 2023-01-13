using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tags.Command.UpdateCommand
{
    public class UpdateTagCommandValidator : AbstractValidator<UpdateTagCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateTagCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.NameTag)
                .NotEmpty().WithMessage("name of the tag can not be empty");

        }
    }
}
