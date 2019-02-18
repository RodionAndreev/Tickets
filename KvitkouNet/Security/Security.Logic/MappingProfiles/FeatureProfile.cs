using AutoMapper;
using Security.Data.Models;
using Security.Logic.Models;

namespace Security.Logic.MappingProfiles
{
    public class FeatureProfile : Profile
    {
        public FeatureProfile()
        {
            CreateMap<Feature, FeatureDb>().ReverseMap();
        }
    }
}
