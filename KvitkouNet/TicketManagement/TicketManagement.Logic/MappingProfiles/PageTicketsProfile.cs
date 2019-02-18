using AutoMapper;
using TicketManagement.Logic.Models;
using Ticket = TicketManagement.Data.DbModels.Ticket;

namespace TicketManagement.Logic.MappingProfiles
{
    /// <summary>
    ///     Настройка маппинга для страниц тикетов
    /// </summary>
    public class PageTicketsProfile : Profile
    {
        public PageTicketsProfile()
        {
            CreateMap<Data.DbModels.Page<Ticket>, Page<TicketLite>>().ReverseMap();
        }
    }
}