using AutoMapper;
using UserSettings.Data.DbModels;
using UserSettings.Logic.Models;

namespace UserSettings.Logic.MappingProfile
{
	public class NotificationsProfile: Profile
	{
		public NotificationsProfile()
		{
			CreateMap<NotificationDb, Notifications>().ReverseMap();
		}
	}
}
