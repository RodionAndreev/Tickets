using FluentValidation;

namespace Notification.Web.Validators.Filters
{
    public class NotificationMessageFilterValidator : AbstractValidator<Logic.Models.NotificationMessage>
    {
        public NotificationMessageFilterValidator()
        {
            RuleFor(x => x.Text)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty();
        }      
    }
}
