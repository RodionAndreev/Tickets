using AutoMapper;
using Dashboard.Data.DbModels;
using Dashboard.Logic.Models;

namespace Dashboard.Logic.MappingProfiles
{
    public class TicketInfoProfile : Profile
    {
        public TicketInfoProfile()
        {
            CreateMap<TicketInfo, TicketInfoDb>().ReverseMap();
        }
    }
}