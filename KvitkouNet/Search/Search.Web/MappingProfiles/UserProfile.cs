using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KvitkouNet.Messages.UserManagement;
using Search.Logic.Common.Models;

namespace Search.Web.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreationMessage, UserInfo>()
                .ForMember(u => u.Rating, opt => opt.MapFrom(u => 0.0))
                .ForMember(u => u.Id, opt => opt.MapFrom(u => u.UserId));

            CreateMap<UserUpdatedMessage, UserInfo>()
               .ForMember(u => u.Id, opt => opt.MapFrom(u => u.UserId));
        }
    }
}
