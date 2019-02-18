using AutoMapper;

namespace Notification.Logic.MappingProfiles
{
	public class EmailNotificationProfile : Profile
	{
		public EmailNotificationProfile()
		{
			CreateMap<Logic.Models.EmailNotification, Data.Models.Notification>()
				.ForMember(data => data.Id,
					opt => opt.MapFrom(logic => logic.NotificationId))
                .ForMember(data => data.Email,
                    opt => opt.MapFrom(logic => logic.Email))
                .ForPath(data => data.Creator,
					opt => opt.MapFrom(logic => logic.Message.Creator))
				.ForPath(data => data.Title,
					opt => opt.MapFrom(logic => logic.Message.Title))
				.ForPath(data => data.Text,
					opt => opt.MapFrom(logic => logic.Message.Text))
				.ForPath(data => data.Severity,
					opt => opt.MapFrom(logic => logic.Message.Severity))
				.ReverseMap()
				.ForMember(logic => logic.NotificationId,
					opt => opt.MapFrom(data => data.Id))
                .ForMember(logic => logic.Email,
                    opt => opt.MapFrom(data => data.Email))
                .ForPath(logic => logic.Message.Creator,
					opt => opt.MapFrom(data => data.Creator))
				.ForPath(logic => logic.Message.Title,
					opt => opt.MapFrom(logic => logic.Title))
				.ForPath(logic => logic.Message.Text,
					opt => opt.MapFrom(data => data.Text))
				.ForPath(logic => logic.Message.Severity,
					opt => opt.MapFrom(data => data.Severity));
		}
	}
}
