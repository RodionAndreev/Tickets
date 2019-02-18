
using UserManagement.Data.DbModels;
using UserManagement.Data.DbModels.Enums;
using UserManagement.Logic.Models;

namespace UserManagement.Logic.MappingProfiles
{
    public class ForUpdateModelProfile : AutoMapper.Profile
    {
        public ForUpdateModelProfile()
        {
            CreateMap<ForUpdateModel, ProfileDB>()
                .ForPath(x => x.FirstName,
                    map => map.MapFrom(u => u.FirstName))
                .ForPath(x => x.LastName,
                    map => map.MapFrom(u => u.LastName))
                .ForPath(x => x.Sex,
                    map => map.MapFrom(u => u.Sex))
                .ForPath(x => x.Birthday,
                    map => map.MapFrom(u => u.Birthday))
                .ReverseMap()
                .ForPath(y => y.FirstName,
                    map => map.MapFrom(u => u.FirstName))
                .ForPath(y => y.LastName,
                    map => map.MapFrom(u => u.LastName))
                .ForPath(y => y.Sex,
                    map => map.MapFrom(u => u.Sex))
                .ForPath(y => y.Birthday,
                    map => map.MapFrom(u => u.Birthday));
        }
    }
}
