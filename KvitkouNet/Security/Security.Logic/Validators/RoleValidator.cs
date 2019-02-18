using FluentValidation;
using Security.Logic.Models;

namespace Security.Logic.Validators
{
    public class RoleValidator : AbstractValidator<Role>
    {
        public RoleValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .Length(1, 100);
        }
    }
}
