using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tags.Command.CreateCommand
{
    public class CreateTagsCommandValidator : AbstractValidator<CreateTagsCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateTagsCommandValidator(IApplicationDbContext context)
        {
            _context = context;
            RuleFor(v => v.NameTag)
                .MaximumLength(50).WithMessage("Tag name must not exceed 50 characters.")
                .MustAsync(TagAlreadyExist).WithMessage("Tag Already Exist");
        }

        private async Task<bool> TagAlreadyExist(string name, CancellationToken cancellationToken)
        {
            return await _context.Tag
               .AllAsync(l => l.NameTag != name, cancellationToken);
        }
    }
}
