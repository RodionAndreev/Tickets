using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Security.Data.ContextModels;
using Security.Data.Models;

namespace Security.Data.MapperProfiles
{
    public class UserRightsProfile : Profile
    {
        public UserRightsProfile()
        {
            CreateMap<UserRights, UserRightsDb>()
                .ForMember(x => x.AccessFunctions, 
                    opt => opt.MapFrom(_ => _.UserRightsAccessFunction
                        .Select(l=>new AccessFunctionDb{
                            Id = l.AccessFunction.Id,
                            Name = l.AccessFunction.Name,
                            FeatureId = l.AccessFunction.FeatureId,
                            AccessRights = l.AccessFunction.AccessFunctionAccessRights.Select(k => new AccessRightDb
                            {
                                Name = k.AccessRight.Name,
                                Id = k.AccessRight.Id
                            } ).ToList()
                        })))
                .ForMember(x => x.AccessRights, 
                    opt => opt.MapFrom(_ => _.UserRightsAccessRight.Where(l=>!l.IsDenied)
                        .Select(l=>new AccessRightDb
                        {
                            Id = l.AccessRight.Id,
                            Name = l.AccessRight.Name,
                        })))
                .ForMember(x => x.DeniedRights, 
                    opt => opt.MapFrom(_ => _.UserRightsAccessRight.Where(l=>l.IsDenied)
                        .Select(l=>new AccessRightDb
                        {
                            Id = l.AccessRight.Id,
                            Name = l.AccessRight.Name,
                        })))
                .ForMember(x => x.Roles, 
                    opt => opt.MapFrom(_ => _.UserRightsRole
                        .Select(l=>new RoleDb
                        {
                            Id = l.Role.Id,
                            Name = l.Role.Name,
                            AccessRights = l.Role.RoleAccessRight.Where(k => !k.IsDenied)
                                .Select(k => new AccessRightDb
                                {
                                    Id = k.AccessRight.Id,
                                    Name = k.AccessRight.Name,
                                }).ToList(),
                            DeniedRights = l.Role.RoleAccessRight.Where(k => k.IsDenied)
                                .Select(k => new AccessRightDb
                                {
                                    Id = k.AccessRight.Id,
                                    Name = k.AccessRight.Name,
                                }).ToList(),
                            AccessFunctions = l.Role.RoleAccessFunction
                                .Select(k => new AccessFunctionDb()
                                {
                                    Id = k.AccessFunction.Id,
                                    Name = k.AccessFunction.Name,
                                    FeatureId = k.AccessFunction.FeatureId,
                                    AccessRights = k.AccessFunction.AccessFunctionAccessRights
                                        .Select(m => new AccessRightDb
                                        {
                                            Id = m.AccessRight.Id,
                                            Name = m.AccessRight.Name,
                                        }).ToList()
                                }).ToList()
                        })))
                .ReverseMap()
                .ForMember(x => x.UserRightsAccessFunction, opt => opt.MapFrom(db => new List<UserRightsAccessFunction>()))
                .ForMember(x => x.UserRightsRole, opt => opt.MapFrom(db => new List<UserRightsRole>()))
                .ForMember(x => x.UserRightsAccessRight, opt => opt.MapFrom(db => new List<UserRightsAccessRight>()));
        }
    }
}
