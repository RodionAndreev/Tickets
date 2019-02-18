using FluentValidation;
using Logging.Logic.Models.Filters;

namespace Logging.Web.Validators.Filters
{
    public class TicketLogsFilterValidator : BaseFilterValidator<TicketLogsFilter>
    {
        public TicketLogsFilterValidator()
        {
            RuleFor(f => f.ActionType)
                .IsInEnum();
        }
    }
}
