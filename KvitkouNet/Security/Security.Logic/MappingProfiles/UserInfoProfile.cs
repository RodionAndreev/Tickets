using AutoMapper;
using Security.Data.Models;
using Security.Logic.Models;

namespace Security.Logic.MappingProfiles
{
    public class UserInfoProfile : Profile
    {
        public UserInfoProfile()
        {
            CreateMap<UserInfo, UserInfoDb>().ReverseMap();
        }
    }
}
