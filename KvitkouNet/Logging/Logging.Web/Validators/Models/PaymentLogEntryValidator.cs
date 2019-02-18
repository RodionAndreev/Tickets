using FluentValidation;
using Logging.Logic.Models;

namespace Logging.Web.Validators.Models
{
    public class PaymentLogEntryValidator : AbstractValidator<PaymentLogEntry>
    {
        public PaymentLogEntryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(e => e.SenderId)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(e => e.ReciverId)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(e => e.Transfer)
                .NotEmpty();

            RuleFor(e => e.EventDate)
                .NotEmpty();
        }
    }
}
