using AutoMapper;
using Security.Data.Models;
using Security.Logic.Models;

namespace Security.Logic.MappingProfiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDb>().ReverseMap();
        }
    }
}
