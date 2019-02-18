using System.Linq;
using AutoMapper;
using Security.Data.ContextModels;
using Security.Data.Models;

namespace Security.Data.MapperProfiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<string, Role>()
                .ForMember(x => x.Name, 
                    opt => opt.MapFrom(_ => _));

            CreateMap<Role, RoleDb>()
                .ForMember(x => x.AccessFunctions,
                    opt => opt.MapFrom(_ => _.RoleAccessFunction
                        .Select(l => new AccessFunctionDb
                        {
                            Id = l.AccessFunction.Id,
                            Name = l.AccessFunction.Name,
                            FeatureId = l.AccessFunction.FeatureId,
                            AccessRights = l.AccessFunction.AccessFunctionAccessRights.Select(k => new AccessRightDb
                            {
                                Name = k.AccessRight.Name,
                                Id = k.AccessRight.Id
                            }).ToList()
                        })))
                .ForMember(x => x.AccessRights,
                    opt => opt.MapFrom(_ => _.RoleAccessRight.Where(l => !l.IsDenied)
                        .Select(l => new AccessRightDb
                        {
                            Id = l.AccessRight.Id,
                            Name = l.AccessRight.Name,
                        })))
                .ForMember(x => x.DeniedRights,
                    opt => opt.MapFrom(_ => _.RoleAccessRight.Where(l => l.IsDenied)
                        .Select(l => new AccessRightDb
                        {
                            Id = l.AccessRight.Id,
                            Name = l.AccessRight.Name,
                        })));
        }
    }
}
