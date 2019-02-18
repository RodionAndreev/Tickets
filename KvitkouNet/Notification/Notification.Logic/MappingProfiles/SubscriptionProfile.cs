using AutoMapper;

namespace Notification.Logic.MappingProfiles
{
	public class SubscriptionProfile : Profile
	{
		public SubscriptionProfile()
		{
            CreateMap<Models.Subscription, Data.Models.Subscription>()
                .ForMember(data => data.Id,
                    opt => opt.MapFrom(logic => logic.Id))
                .ForPath(data => data.Creator,
                    opt => opt.MapFrom(logic => logic.Creator))
                .ForPath(data => data.Theme,
                    opt => opt.MapFrom(logic => logic.Theme))
                .ReverseMap()
                .ForMember(logic => logic.Id,
                    opt => opt.MapFrom(data => data.Id))
                .ForMember(logic => logic.Creator,
                    opt => opt.MapFrom(data => data.Creator))
                .ForPath(logic => logic.Theme,
                    opt => opt.MapFrom(data => data.Theme));
		}
	}
}
