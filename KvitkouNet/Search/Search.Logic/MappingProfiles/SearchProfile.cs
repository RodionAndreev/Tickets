using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Search.Data.Models;
using Search.Logic.Common.Models;

namespace Search.Logic.MappingProfiles
{
    public class SearchProfile : Profile
    {
        public SearchProfile()
        {
            CreateMap<TicketSearchRequest, TicketSearchEntity>()
                .ForMember(entity => entity.SearchTime,
                    opt => opt.MapFrom(request => DateTime.UtcNow));

            CreateMap<UserSearchRequest, UserSearchEntity>()
                .ForMember(entity => entity.SearchTime,
                    opt => opt.MapFrom(request => DateTime.UtcNow));
        }
    }
}
