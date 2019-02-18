using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KvitkouNet.Messages.TicketManagement;
using Search.Logic.Common.Models;

namespace Search.Web.MappingProfiles
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<TicketCreationMessage, TicketInfo>()
                .ForMember(u => u.Id, opt => opt.MapFrom(u => u.TicketId));

            CreateMap<TicketUpdatedMessage, TicketInfo>()
                .ForMember(u => u.Id, opt => opt.MapFrom(u => u.TicketId));
        }
    }
}
