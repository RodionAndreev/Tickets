using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notification.Web.Validators.Filters
{
    public class UserNotificationBulkRequestFilterValidator : AbstractValidator<Logic.Models.Requests.UserNotificationBulkRequest>
    {
        public UserNotificationBulkRequestFilterValidator()
        {
            var rul = RuleFor(x => x.UserIds)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Message)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Message.Text)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Message.Title)
                .NotNull()
                .NotEmpty();

        }
    }
}
