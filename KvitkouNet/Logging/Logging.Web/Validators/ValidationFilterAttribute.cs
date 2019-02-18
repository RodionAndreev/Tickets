using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Logging.Web.Validators
{
    public class ValidationFilterAttribute : ActionFilterAttribute
    {
        private readonly IValidatorFactory _factory;

        public ValidationFilterAttribute(IValidatorFactory factory)
        {
            _factory = factory;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            
            if (context.ActionArguments.Values.All(value => value == null))
            {
                context.Result = new BadRequestResult();
                return;
            }

            var errorMessages = new List<string>();

            foreach (var arg in context.ActionArguments)
            {
                if (arg.Value == null)
                {
                    continue;
                }

                var validator = _factory.GetValidator(arg.Value.GetType());
                
                var validationResult = validator?.Validate(arg.Value);
                
                if (validationResult == null || validationResult.IsValid)
                {
                    continue;
                }

                errorMessages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            if (errorMessages.Any())
            {
                context.Result = new BadRequestObjectResult(errorMessages);
            }
        }
    }
}
