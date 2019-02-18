using FluentValidation;
using TicketManagement.Logic.Models;

namespace TicketManagement.Logic.Validators
{
    /// <summary>
    ///     Настройка валидации для адресса
    /// </summary>
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(address => address.City).NotEmpty().Length(3, 15);
            RuleFor(address => address.Country).NotEmpty().Length(3, 15);
            RuleFor(address => address.Street).NotEmpty().Length(3, 15);
            RuleFor(address => address.House).NotEmpty().Length(1, 6);
            RuleFor(address => address.Flat).Length(1, 6);
        }
    }
}