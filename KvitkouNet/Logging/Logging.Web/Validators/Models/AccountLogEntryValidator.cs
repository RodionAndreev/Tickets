using FluentValidation;
using Logging.Logic.Models;

namespace Logging.Web.Validators.Models
{
    public class AccountLogEntryValidator : AbstractValidator<AccountLogEntry>
    {
        public AccountLogEntryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(e => e.UserId)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(e => e.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(e => e.Type)
                .NotEmpty();

            RuleFor(e => e.EventDate)
                .NotEmpty();
        }
    }
}
