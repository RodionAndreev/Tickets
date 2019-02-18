using AutoMapper;
using Security.Data.Models;
using Security.Logic.Models;

namespace Security.Logic.MappingProfiles
{
    public class AccessFunctionProfile : Profile
    {
        public AccessFunctionProfile()
        {
            CreateMap<AccessFunction, AccessFunctionDb>().ReverseMap();
        }
    }
}
