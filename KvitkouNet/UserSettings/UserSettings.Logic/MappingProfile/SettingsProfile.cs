using AutoMapper;
using UserSettings.Data.DbModels;
using UserSettings.Logic.Models;

namespace UserSettings.Logic.MappingProfile
{
	public class SettingsProfile: Profile
	{
		public SettingsProfile()
		{
			CreateMap<Settings, SettingsDb>().ReverseMap();
		}
	}
}
