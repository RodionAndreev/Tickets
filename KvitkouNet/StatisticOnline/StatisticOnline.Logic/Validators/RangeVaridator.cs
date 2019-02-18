using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using StatisticOnline.Logic.Models;

namespace StatisticOnline.Logic.Validators
{
    class RangeVaridator: AbstractValidator<DateRange>
    {
        public RangeVaridator()
        {
            var message = "this is a required field";
            RuleFor(_ => _.StartDate).NotEmpty().WithMessage(message);
            RuleFor(_ => _.EndDate).NotEmpty().WithMessage(message);

        }
    }
}
