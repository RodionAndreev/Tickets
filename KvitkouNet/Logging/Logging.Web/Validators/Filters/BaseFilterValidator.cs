using FluentValidation;
using Logging.Logic.Models.Filters.Abstraction;

namespace Logging.Web.Validators.Filters
{
    public class BaseFilterValidator<T> : AbstractValidator<T> where T : BaseLogFilter
    {
        public BaseFilterValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            When(f => f.DateFrom.HasValue && f.DateTo.HasValue, () =>
            {
                RuleFor(f => f.DateFrom.Value)
                    .LessThanOrEqualTo(f => f.DateTo.Value);
            });
        }
    }
}
