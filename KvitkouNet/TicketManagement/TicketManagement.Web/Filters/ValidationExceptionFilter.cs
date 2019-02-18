using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TicketManagement.Web.Filters
{
    public class ValidationExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationException)
            {
                var errors = context.Exception as ValidationException;

                context.Result = new BadRequestObjectResult(errors.Errors);
                context.ExceptionHandled = true;
            }
        }
    }
}