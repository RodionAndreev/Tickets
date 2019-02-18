using FluentValidation;
using Logging.Logic.Models.Filters;

namespace Logging.Web.Validators.Filters
{
    public class DealLogFilterValidator : BaseFilterValidator<DealLogFilter>
    {
        public DealLogFilterValidator()
        {
            RuleFor(f => f.Type)
                .IsInEnum();

            When(f => f.MinPrice.HasValue, () =>
            {
                RuleFor(f => f.MinPrice.Value)
                    .GreaterThanOrEqualTo(default(double));
            });

            When(f => f.MinPrice.HasValue && f.MaxPrice.HasValue, () =>
            {
                RuleFor(f => f.MinPrice.Value)
                    .LessThanOrEqualTo(f => f.MaxPrice.Value);
            });
        }
    }
}
