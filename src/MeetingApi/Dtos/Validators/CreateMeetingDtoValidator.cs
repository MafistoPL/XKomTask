using FluentValidation;
using Persistence.EF.Entities;
using Persistence.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingApi.Dtos.Validators
{
    public class CreateMeetingDtoValidator : AbstractValidator<CreateMeetingDto>
    {
        public CreateMeetingDtoValidator()
        {
            RuleFor(m => new { m.StartDate, m.EndDate })
                .Custom((value, context) =>
                {
                    if (value.StartDate > value.EndDate)
                    {
                        context.AddFailure("The start date cannot be later than the end date");
                    }
                    else if (value.StartDate == value.EndDate)
                    {
                        context.AddFailure("The start date cannot be the same as the end date");
                    }
                });
        }
    }
}
