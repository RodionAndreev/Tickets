using FluentValidation;
using Logging.Logic.Models;

namespace Logging.Web.Validators.Models
{
    public class DealLogEntryValidator : AbstractValidator<TicketDealLogEntry>
    {
        public DealLogEntryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(e => e.TicketId)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(e => e.OwnerId)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(e => e.RecieverId)
                .NotEmpty()
                .MinimumLength(3);

            When(e => e.Price.HasValue, () =>
            {
                RuleFor(e => e.Price.Value)
                    .GreaterThanOrEqualTo(default(double));
            });

            RuleFor(e => e.Type)
                .NotEmpty();

            RuleFor(e => e.EventDate)
                .NotEmpty();
        }
    }
}
