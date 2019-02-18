using AutoMapper;
using KvitkouNet.Messages.Logging;
using Logging.Logic.Models;

namespace Logging.Logic.MappingProfiles.MessageToDomainReverse
{
	public class TicketActionLogMsgToDomainProfile : Profile
	{
		public TicketActionLogMsgToDomainProfile()
		{
			CreateMap<TicketActionLogMessage, TicketActionLogEntry>()
				.ReverseMap();
		}
	}
}