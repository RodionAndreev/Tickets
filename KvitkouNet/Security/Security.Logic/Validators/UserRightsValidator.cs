using FluentValidation;
using Security.Logic.Models;

namespace Security.Logic.Validators
{
    public class UserRightsValidator : AbstractValidator<UserRights>
    {
        public UserRightsValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .Length(1, 100);
            RuleFor(x => x.UserLogin)
                .NotNull()
                .Length(1, 100);
        }
    }
}
