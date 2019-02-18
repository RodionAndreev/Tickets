using KvitkouNet.Messages.Logging;
using System;
using Profile = AutoMapper.Profile;

namespace AdminPanel.Logic.MappingProfiles
{
	public class ExceptionToInternalErrorLogMessageProfile : Profile
	{
		public ExceptionToInternalErrorLogMessageProfile()
		{
			CreateMap<Exception, InternalErrorLogMessage>()
				.ForMember(_ => _.ServiceName, opts => opts.MapFrom(src => "AdminPanel"))
				.ForMember(_ => _.Message, opts => opts.MapFrom(src => src.Message))
				.ForMember(_ => _.HResult, opts => opts.MapFrom(src => src.HResult))
				.ForMember(_ => _.InnerExceptionString, opts => opts.MapFrom(src => src.InnerException.ToString()))
				.ForMember(_ => _.Source, opts => opts.MapFrom(src => src.Source))
				.ForMember(_ => _.StackTrace, opts => opts.MapFrom(src => src.StackTrace))
				.ForMember(_ => _.TargetSiteName, opts => opts.MapFrom(src => src.TargetSite.Name))
				.ForMember(_ => _.ExceptionType, opts => opts.MapFrom(src => src.GetType().ToString()));
		}
	}
}