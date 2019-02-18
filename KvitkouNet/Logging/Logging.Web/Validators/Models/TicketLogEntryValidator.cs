using FluentValidation;
using Logging.Logic.Models;

namespace Logging.Web.Validators.Models
{
    public class TicketLogEntryValidator : AbstractValidator<TicketActionLogEntry>
    {
        public TicketLogEntryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(e => e.UserId)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(e => e.TicketId)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(e => e.ActionType)
                .NotEmpty();

            RuleFor(e => e.EventDate)
                .NotEmpty();
        }
    }
}
