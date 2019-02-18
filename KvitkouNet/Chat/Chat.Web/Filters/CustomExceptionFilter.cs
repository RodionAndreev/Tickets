using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Chat.Web.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        //добавим общий фильтр
        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(Exception))
            {
                context.Result = new BadRequestObjectResult(context.Exception.Message);
                context.ExceptionHandled = true;
            }
        }
    }
}
