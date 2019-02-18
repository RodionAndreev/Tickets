using FluentValidation;
using Logging.Logic.Models;

namespace Logging.Web.Validators.Models
{
    public class SearchQueryLogEntryValidator : AbstractValidator<SearchQueryLogEntry>
    {
        public SearchQueryLogEntryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(e => e.UserId)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(e => e.SearchCriterium)
                .NotEmpty();

            RuleFor(e => e.EventDate)
                .NotEmpty();
        }
    }
}
