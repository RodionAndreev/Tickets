using AutoMapper;
using Logging.Data.DbModels;
using Logging.Logic.Enums;
using Logging.Logic.Models;

namespace Logging.Logic.MappingProfiles.DomainToDbReverse
{
	public class AccountLogDomainToDbProfile : Profile
	{
		public AccountLogDomainToDbProfile()
		{
			CreateMap<AccountLogEntry, AccountLogEntryDbModel>()
			    .ForMember(dbm => dbm.Type, opts => opts.MapFrom(m => (int) m.Type))
				.ReverseMap()
			    .ForMember(m => m.Type, opts => opts.MapFrom(dbm => (AccountActionType) dbm.Type));
		}
	}
}