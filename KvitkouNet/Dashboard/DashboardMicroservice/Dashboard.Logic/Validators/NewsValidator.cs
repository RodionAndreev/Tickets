using FluentValidation;
using Dashboard.Logic.Models;

namespace Dashboard.Logic.Validators
{
    public class NewsValidator : AbstractValidator<News>
    {
        public NewsValidator()
        {
            RuleFor(news => news.Ticket.TicketId).NotEmpty();
            RuleFor(news => news.Ticket.Name).NotEmpty().Length(2, 100);
            RuleFor(news => news.Ticket.City).NotEmpty().Length(2, 100);
            RuleFor(news => news.Ticket.Category).NotEmpty();
        }
    }
}
