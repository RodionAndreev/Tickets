using AutoMapper;
using TicketManagement.Logic.Models;

namespace TicketManagement.Logic.MappingProfiles
{
    /// <summary>
    ///     Настройка маппинга для тикетов
    /// </summary>
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, Data.DbModels.Ticket>().ReverseMap();
            CreateMap<Data.DbModels.Ticket, TicketLite>().ReverseMap();
        }
    }
}