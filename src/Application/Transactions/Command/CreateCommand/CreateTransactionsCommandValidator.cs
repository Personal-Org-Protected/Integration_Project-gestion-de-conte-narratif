using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Transactions.Command.CreateCommand
{
    public class CreateTransactionsCommandValidator : AbstractValidator<CreateTransactionsCommand>
    {
        public CreateTransactionsCommandValidator()
        {
            RuleFor(v=>v.NameBook)
                .NotEmpty()
                .WithMessage("this transaction must have a book Name");
            RuleFor(v => v.price)
              .NotEmpty()
              .WithMessage("this transaction must have a price");
            RuleFor(v => v.StoryTellId)
              .NotEmpty()
              .WithMessage("this transaction must be linked to a story");

        }
    }
}
