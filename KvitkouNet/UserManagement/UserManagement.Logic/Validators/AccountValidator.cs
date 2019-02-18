using FluentValidation;
using UserManagement.Logic.Models;

namespace UserManagement.Logic.Validators
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            //RuleFor(x => x.Email)
            //    .NotEmpty();
        }
    }
}
