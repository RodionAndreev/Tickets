using FluentValidation;
using Security.Logic.Models;

namespace Security.Logic.Validators
{
    public class FeatureValidator : AbstractValidator<Feature>
    {
        public FeatureValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .Length(1, 100);
        }
    }
}
