using AutoMapper;
using Security.Data.Models;
using Security.Logic.Models;

namespace Security.Logic.MappingProfiles
{
    public class AccessRightProfile : Profile
    {
        public AccessRightProfile()
        {
            CreateMap<AccessRight, AccessRightDb>().ReverseMap();
        }
    }
}
