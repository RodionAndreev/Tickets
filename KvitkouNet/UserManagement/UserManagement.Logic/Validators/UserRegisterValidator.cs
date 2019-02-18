using FluentValidation;
using UserManagement.Logic.Models;

namespace UserManagement.Logic.Validators
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterModel>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().Length(4, 15)
            .Matches(@"^[a-zA-Z0-9_-]");
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).NotEmpty().Length(6, 12).Matches(@"^[a-zA-Z0-9_-]");
            
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);
        }
    }
}
