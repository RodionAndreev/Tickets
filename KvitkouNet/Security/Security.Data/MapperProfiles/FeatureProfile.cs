using System.Linq;
using AutoMapper;
using Security.Data.ContextModels;
using Security.Data.Models;

namespace Security.Data.MapperProfiles
{
    public class FeatureProfile : Profile
    {
        public FeatureProfile()
        {
            CreateMap<string, Feature>()
                .ForMember(x => x.Name, 
                    opt => opt.MapFrom(_ => _));
            CreateMap<Feature, FeatureDb>()
                .ForMember(x => x.AvailableAccessRights,
                    opt => opt.MapFrom(_ => _.FeatureAccessRight
                        .Select(l => new AccessRightDb
                        {
                            Id = l.AccessRight.Id,
                            Name = l.AccessRight.Name
                        })));
        }
    }
}
