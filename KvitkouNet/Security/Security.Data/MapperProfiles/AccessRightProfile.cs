using AutoMapper;
using Security.Data.ContextModels;

namespace Security.Data.MapperProfiles
{
    public class AccessRightProfile : Profile
    {
        public AccessRightProfile()
        {
            CreateMap<string, AccessRight>()
                .ForMember(x => x.Name,
                    opt => opt.MapFrom(_ => _));
        }
    }
}
