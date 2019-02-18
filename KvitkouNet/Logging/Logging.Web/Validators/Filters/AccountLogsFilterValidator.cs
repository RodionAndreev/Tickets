using FluentValidation;
using Logging.Logic.Models.Filters;

namespace Logging.Web.Validators.Filters
{
    public class AccountLogsFilterValidator : BaseFilterValidator<AccountLogsFilter>
    {
        public AccountLogsFilterValidator()
        {
            RuleFor(f => f.Type)
                .IsInEnum();
        }
    }
}
