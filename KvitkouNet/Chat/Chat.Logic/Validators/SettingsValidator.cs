using System;
using Chat.Logic.Models;
using FluentValidation;

namespace Chat.Logic.Validators
{
    public class SettingsValidator : AbstractValidator<Settings>
    {
        public SettingsValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty();
        }
    }
}
