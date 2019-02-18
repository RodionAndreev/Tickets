using FluentValidation;
using Security.Logic.Models;

namespace Security.Logic.Validators
{
    public class AccessFunctionValidator : AbstractValidator<AccessFunction>
    {
        public AccessFunctionValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .Length(1, 100);
            RuleFor(x => x.FeatureId)
                .GreaterThan(0);
        }
    }
}
