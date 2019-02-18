using AutoMapper;
using UserSettings.Data.DbModels;
using UserSettings.Logic.Models;

namespace UserSettings.Logic.MappingProfile
{
	public class VisibleProfile: Profile
	{
		public VisibleProfile()
		{
			CreateMap<VisibleInfoDb, VisibleInfo>().ReverseMap();
		}
	}
}
