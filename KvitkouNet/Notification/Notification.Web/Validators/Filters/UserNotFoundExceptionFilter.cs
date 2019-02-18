using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Notification.Logic.Exceptions;

namespace Notification.Web.Validators.Filters
{
    public class UserNotFoundExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is UserNotFound)
            {                
                context.Result = new BadRequestObjectResult(context.Exception.Message);               
                context.ExceptionHandled = true;
            }
        }
    }
}
