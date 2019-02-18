using AutoMapper;
using Logging.Data.DbModels;
using Logging.Logic.Enums;
using Logging.Logic.Models;

namespace Logging.Logic.MappingProfiles.DomainToDbReverse
{
	public class TicketActionLogDomainToDbProfile : Profile
	{
		public TicketActionLogDomainToDbProfile()
		{
			CreateMap<TicketActionLogEntry, TicketActionLogEntryDbModel>()
			    .ForMember(dbm => dbm.Type, opts => opts.MapFrom(m => (int) m.ActionType))
				.ReverseMap()
			    .ForMember(m =>m.ActionType, opts => opts.MapFrom(dbm => (TicketActionType) dbm.Type));
		}
	}
}