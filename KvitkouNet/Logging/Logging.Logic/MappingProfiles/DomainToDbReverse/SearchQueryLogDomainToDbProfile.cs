using AutoMapper;
using Logging.Data.DbModels;
using Logging.Logic.Models;

namespace Logging.Logic.MappingProfiles.DomainToDbReverse
{
	public class SearchQueryLogDomainToDbProfile : Profile
	{
		public SearchQueryLogDomainToDbProfile()
		{
			CreateMap<SearchQueryLogEntry, SearchQueryLogEntryDbModel>()
				.ReverseMap();
		}
	}
}