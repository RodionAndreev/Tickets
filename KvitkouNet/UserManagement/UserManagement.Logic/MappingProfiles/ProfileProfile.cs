using UserManagement.Data.DbModels;

namespace UserManagement.Logic.MappingProfiles
{
    public class ProfileProfile : AutoMapper.Profile
    {
        public ProfileProfile()
        {
            CreateMap<Models.Profile, ProfileDB>()
                .ReverseMap();
        }
    }
}
