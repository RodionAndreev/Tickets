using AutoMapper;
using Security.Data.Models;
using Security.Logic.Models;

namespace Security.Logic.MappingProfiles
{
    public class UserRightsProfile : Profile
    {
        public UserRightsProfile()
        {
            CreateMap<UserRights, UserRightsDb>().ReverseMap();
        }
    }
}
