using UserManagement.Data.DbModels;
using UserManagement.Logic.Models;

namespace UserManagement.Logic.MappingProfiles
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDB>()
                .ForMember(x => x.Id,
                    map => map.MapFrom(u => u.Id))
                .ForMember(x => x.AccountDB,
                    opt => opt.MapFrom(_ => _.Account))
                .ForPath(x => x.AccountDB.Login,
                    map => map.MapFrom(u => u.Account.Login))
                .ForMember(x => x.ProfileDB,
                    opt => opt.MapFrom(_ => _.Profile))
                .ReverseMap();
        }
    }
}
