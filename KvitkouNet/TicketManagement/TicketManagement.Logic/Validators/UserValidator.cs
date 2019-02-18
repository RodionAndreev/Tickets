using FluentValidation;
using TicketManagement.Logic.Models;

namespace TicketManagement.Logic.Validators
{
    /// <summary>
    ///     Настройка валидатора для юзера
    /// </summary>
    public class UserValidator : AbstractValidator<UserInfo>
    {
        public UserValidator()
        {
            RuleFor(ticket => ticket.FirstName).NotEmpty().Length(3, 15);
            RuleFor(ticket => ticket.LastName).Length(3, 15);
        }
    }
}