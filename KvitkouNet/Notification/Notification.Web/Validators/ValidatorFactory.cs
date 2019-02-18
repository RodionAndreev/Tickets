using FluentValidation;
using System;

namespace Notification.Web.Validators
{
    public class ValidatorFactory : ValidatorFactoryBase
    {
        private readonly IServiceProvider m_services;

        public ValidatorFactory(IServiceProvider services)
        {
            m_services = services;            
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            IValidator validator = (IValidator)m_services.GetService(validatorType);
            return validator;
        }
    }
}
