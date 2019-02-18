using System.Linq;
using AutoMapper;
using Security.Data.ContextModels;
using Security.Data.Models;

namespace Security.Data.MapperProfiles
{
    public class AccessFunctionProfile : Profile
    {
        public AccessFunctionProfile()
        {
            CreateMap<AccessFunction, AccessFunctionDb>()
                .ForMember(x => x.AccessRights, opt => opt.MapFrom(_ => _.AccessFunctionAccessRights
                    .Select(l => new AccessRightDb
                    {
                        Id = l.AccessRight.Id,
                        Name = l.AccessRight.Name
                    })))
                .ForMember(x=>x.FeatureName, opt => opt.MapFrom(_ => _.Feature.Name));
        }
    }
}
