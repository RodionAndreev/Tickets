using FluentValidation;
using Security.Logic.Models.Requests;

namespace Security.Logic.Validators
{
    public class AccessRequestValidator : AbstractValidator<CheckAccessRequest>
    {
        public AccessRequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .Length(1, 100);
            RuleFor(x => x.AccessRightNames)
                .NotNull()
                .NotEmpty();
            RuleForEach(x => x.AccessRightNames)
                .NotNull()
                .NotEmpty()
                .Length(1, 100);
        }
    }
}
