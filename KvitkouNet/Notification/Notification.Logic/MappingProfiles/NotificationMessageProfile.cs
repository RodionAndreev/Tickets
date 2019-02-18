using AutoMapper;

namespace Notification.Logic.MappingProfiles
{
	public class NotificationMessageProfile : Profile
	{
		public NotificationMessageProfile()
		{
			CreateMap<Logic.Models.NotificationMessage, Data.Models.Notification>()
                .ForMember(data => data.Creator,
                    opt => opt.MapFrom(logic => logic.Creator))
				.ForMember(data => data.Title,
					opt => opt.MapFrom(logic => logic.Title))
				.ForMember(data => data.Text,
					opt => opt.MapFrom(logic => logic.Text))
				.ForMember(data => data.Severity,
					opt => opt.MapFrom(logic => logic.Severity))
				.ReverseMap()
                .ForMember(logic => logic.Creator,
                    opt => opt.MapFrom(data => data.Creator))
				.ForMember(logic => logic.Title,
					opt => opt.MapFrom(logic => logic.Title))
				.ForMember(logic => logic.Text,
					opt => opt.MapFrom(data => data.Text))
				.ForMember(logic => logic.Severity,
					opt => opt.MapFrom(data => data.Severity));
		}
	}
}
