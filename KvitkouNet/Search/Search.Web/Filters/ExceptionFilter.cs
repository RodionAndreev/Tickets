using System;
using EasyNetQ;
using KvitkouNet.Messages.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Search.Web.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly IBus _bus;

        public ExceptionFilter(IBus bus)
        {
            _bus = bus;
        }

        public void OnException(ExceptionContext context)
        {
            _bus.Publish(new InternalErrorLogMessage
            {
                ServiceName = "SearchMicroService",
                ExceptionType = context.Exception.GetType().FullName,
                InnerExceptionString = context.Exception.InnerException?.ToString(),
                HResult = context.Exception.HResult,
                Message = context.Exception.Message,
                StackTrace = context.Exception.StackTrace,
                TargetSiteName = context.Exception.TargetSite.Name,
                Source = context.Exception.Source
            });

            context.Result = new BadRequestObjectResult(context.Exception.Message);
            context.ExceptionHandled = true;
        }
    }
}
