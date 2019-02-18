using AutoMapper;
using KvitkouNet.Messages.Logging;
using Logging.Logic.Models;

namespace Logging.Logic.MappingProfiles.MessageToDomainReverse
{
	public class InternalErrorLogMsgToDomainProfile : Profile
	{
		public InternalErrorLogMsgToDomainProfile()
		{
			CreateMap<InternalErrorLogMessage, InternalErrorLogEntry>()
				.ReverseMap();
		}
		
	}
}