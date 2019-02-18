using AutoMapper;
using KvitkouNet.Messages.Logging;
using Logging.Logic.Models;

namespace Logging.Logic.MappingProfiles.MessageToDomainReverse
{
	public class TicketDealLogMsgToDomainProfile : Profile
	{
		public TicketDealLogMsgToDomainProfile()
		{
			CreateMap<TicketDealLogMessage, TicketDealLogEntry>()
				.ReverseMap();
		}
	}
}