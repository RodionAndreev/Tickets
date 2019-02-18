using System;
using Chat.Logic.Models;
using FluentValidation;

namespace Chat.Logic.Validators
{
    public class RoomValidator : AbstractValidator<Room>
    {
        public RoomValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty();

            RuleFor(x => x.Name)
                .NotNull().NotEmpty();
        }
    }
}
