using FluentValidation;
using Logging.Logic.Models;

namespace Logging.Web.Validators.Models
{
    public class ErrorLogEntryValidator : AbstractValidator<InternalErrorLogEntry>
    {
        public ErrorLogEntryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(e => e.ServiceName)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(e => e.ExceptionType)
                .NotEmpty();

            RuleFor(e => e.Message)
                .NotEmpty();

            RuleFor(e => e.EventDate)
                .NotEmpty();
        }
    }
}
