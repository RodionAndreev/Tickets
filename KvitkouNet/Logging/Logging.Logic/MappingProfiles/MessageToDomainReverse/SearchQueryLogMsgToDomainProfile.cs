using AutoMapper;
using KvitkouNet.Messages.Logging;
using Logging.Logic.Models;

namespace Logging.Logic.MappingProfiles.MessageToDomainReverse
{
	public class SearchQueryLogMsgToDomainProfile : Profile
	{
		public SearchQueryLogMsgToDomainProfile()
		{
			CreateMap<SearchQueryLogMessage, SearchQueryLogEntry>()
				.ReverseMap();
		}
	}
}