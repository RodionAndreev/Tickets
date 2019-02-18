using AutoMapper;

namespace Notification.Logic.MappingProfiles
{
    public class SeverityProfile : Profile
    {
        public SeverityProfile()
        {
            CreateMap<Logic.Models.Enums.Severity, Data.Models.Enums.Severity>().ReverseMap();
        }
    }
}
