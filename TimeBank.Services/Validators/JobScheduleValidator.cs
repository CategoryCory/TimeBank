using FluentValidation;
using TimeBank.Repository.Models;

namespace TimeBank.Services.Validators
{
    public class JobScheduleValidator : AbstractValidator<JobSchedule>
    {
        public JobScheduleValidator()
        {
            RuleFor(s => s.DayOfWeek)
                .NotNull()
                .InclusiveBetween(0, 6)
                .WithMessage("The value must be between zero and six, inclusively.");
            RuleFor(s => s.TimeBegin)
                .NotNull()
                .WithMessage("The time begin value must not be null.");
            RuleFor(s => s.TimeEnd)
                .NotNull()
                .WithMessage("The end time value must not be null.");
            RuleFor(s => s.JobId)
                .NotNull()
                .WithMessage("The job id value must not be null.");
        }
    }
}
