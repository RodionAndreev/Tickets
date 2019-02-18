using AutoMapper;
using Logging.Data.DbModels;
using Logging.Logic.Models;

namespace Logging.Logic.MappingProfiles.DomainToDbReverse
{
	public class InternalErrorLogDomainToDbProfile : Profile
	{
		public InternalErrorLogDomainToDbProfile()
		{
			CreateMap<InternalErrorLogEntry, InternalErrorLogEntryDbModel>()
				.ReverseMap();
		}
	}
}