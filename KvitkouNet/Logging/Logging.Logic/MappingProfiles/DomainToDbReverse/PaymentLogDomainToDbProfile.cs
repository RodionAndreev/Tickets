using AutoMapper;
using Logging.Data.DbModels;
using Logging.Logic.Models;

namespace Logging.Logic.MappingProfiles.DomainToDbReverse
{
	public class PaymentLogDomainToDbProfile : Profile
	{
		public PaymentLogDomainToDbProfile()
		{
			CreateMap<PaymentLogEntry, PaymentLogEntryDbModel>()
				.ReverseMap();
		}
	}
}