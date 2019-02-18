using FluentValidation;
using Logging.Logic.Models.Filters;

namespace Logging.Web.Validators.Filters
{
    public class PaymentLogsFilterValidator : BaseFilterValidator<PaymentLogsFilter>
    {
        public PaymentLogsFilterValidator()
        {
            When(f => f.MinTransfer.HasValue, () =>
            {
                RuleFor(f => f.MinTransfer.Value)
                    .GreaterThanOrEqualTo(default(double));
            });

            When(f => f.MinTransfer.HasValue && f.MaxTransfer.HasValue, () =>
            {
                RuleFor(f => f.MinTransfer.Value)
                    .LessThanOrEqualTo(f => f.MaxTransfer.Value);
            });
        }
    }
}
