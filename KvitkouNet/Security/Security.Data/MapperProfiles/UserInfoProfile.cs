using AutoMapper;
using Security.Data.ContextModels;
using Security.Data.Models;

namespace Security.Data.MapperProfiles
{
    public class UserInfoProfile : Profile
    {
        public UserInfoProfile()
        {
            CreateMap<UserRights, UserInfoDb>()
                .ReverseMap()
                .ForMember(x => x.UserRightsAccessFunction, opt => opt.Ignore())
                .ForMember(x => x.UserRightsRole, opt => opt.Ignore())
                .ForMember(x => x.UserRightsAccessRight, opt => opt.Ignore());
        }
    }
}
