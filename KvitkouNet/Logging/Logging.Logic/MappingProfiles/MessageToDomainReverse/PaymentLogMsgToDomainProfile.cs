using AutoMapper;
using KvitkouNet.Messages.Logging;
using Logging.Logic.Models;

namespace Logging.Logic.MappingProfiles.MessageToDomainReverse
{
	public class PaymentLogMsgToDomainProfile : Profile
	{
		public PaymentLogMsgToDomainProfile()
		{
			CreateMap<PaymentLogMessage, PaymentLogEntry>()
				.ReverseMap();
		}
	}
}