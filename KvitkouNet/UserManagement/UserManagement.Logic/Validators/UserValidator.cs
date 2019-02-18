using FluentValidation;
using UserManagement.Logic.Models;

namespace UserManagement.Logic.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Account)
                .NotEmpty();
        }
    }
}
