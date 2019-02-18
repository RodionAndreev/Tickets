using AutoMapper;
using KvitkouNet.Messages.Logging;
using Logging.Logic.Models;

namespace Logging.Logic.MappingProfiles.MessageToDomainReverse
{
	public class AccountLogMsgToDomainProfile : Profile
	{
		public AccountLogMsgToDomainProfile()
		{
			CreateMap<AccountLogMessage, AccountLogEntry>()
				.ReverseMap();
		}
	}
}