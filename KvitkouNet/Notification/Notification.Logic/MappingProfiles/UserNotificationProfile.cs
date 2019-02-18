using AutoMapper;
using Notification.Logic.Models;

namespace Notification.Logic.MappingProfiles
{
	public class UserNotificationProfile : Profile
	{
		public UserNotificationProfile()
		{
			CreateMap<Logic.Models.UserNotification, Data.Models.Notification>()
				.ForMember(data => data.Id,
					opt => opt.MapFrom(logic => logic.NotificationId))
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
                .ForPath(logic => logic.Message.Creator,
                    opt => opt.MapFrom(logic => logic.Creator))
                .ForPath(logic => logic.Message.Title,
					opt => opt.MapFrom(logic => logic.Title))
				.ForPath(logic => logic.Message.Text,
					opt => opt.MapFrom(data => data.Text))
				.ForPath(logic => logic.Message.Severity,
					opt => opt.MapFrom(data => data.Severity));
		}
	}
}
