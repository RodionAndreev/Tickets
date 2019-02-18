using FluentValidation;
using TicketManagement.Logic.Models;

namespace TicketManagement.Logic.Validators
{
    /// <summary>
    ///     Настройка валидации
    /// </summary>
    public class TicketValidator : AbstractValidator<Ticket>
    {
        public TicketValidator()
        {
            RuleFor(ticket => ticket.Name).NotEmpty().Length(5, 100);
            RuleFor(ticket => ticket.AdditionalData).MaximumLength(240);
            RuleFor(ticket => ticket.SellerPhone).NotEmpty().Length(6, 20)
                .Matches(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$");
            RuleFor(ticket => ticket.LocationEvent).SetValidator(new AddressValidator());
            RuleFor(ticket => ticket.SellerAdress).SetValidator(new AddressValidator());
            RuleFor(ticket => ticket.TypeEvent).NotEmpty();
        }
    }
}