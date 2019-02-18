using AutoMapper;
using Logging.Data.DbModels;
using Logging.Logic.Enums;
using Logging.Logic.Models;

namespace Logging.Logic.MappingProfiles.DomainToDbReverse
{
	public class TicketDealLogDomainToDbProfile : Profile
	{
		public TicketDealLogDomainToDbProfile()
		{
			CreateMap<TicketDealLogEntry, TicketDealLogEntryDbModel>()
			    .ForMember(dbm => dbm.Type, opts => opts.MapFrom(m => (int) m.Type))
				.ReverseMap()
			    .ForMember(m => m.Type, opts => opts.MapFrom(dbm => (DealType) dbm.Type));
		}
	}
}