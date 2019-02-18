using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TicketManagement.Logic.Exceptions;

namespace TicketManagement.Web.Filters
{
    public class EasyNetQSendExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is EasyNetQSendException)
            {
                var res = context.Exception as EasyNetQSendException;
                if (res.resultResponse != null)
                    context.Result = new OkObjectResult(res.resultResponse);
                else
                    context.Result = new NoContentResult();
                context.ExceptionHandled = true;
            }
        }
    }
}