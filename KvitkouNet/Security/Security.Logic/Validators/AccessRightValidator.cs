using FluentValidation;
using Security.Logic.Models;

namespace Security.Logic.Validators
{
    public class AccessRightValidator : AbstractValidator<AccessRight>
    {
        public AccessRightValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .Length(1, 100);
        }
    }
}
