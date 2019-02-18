using System;
using FluentValidation;

namespace Logging.Web.Validators
{
    public class ValidatorFactory : ValidatorFactoryBase
    {
        private readonly IServiceProvider _services;

        public ValidatorFactory(IServiceProvider services)
        {
            _services = services;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            return (IValidator) _services.GetService(validatorType);
        }
    }
}
