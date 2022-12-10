using System.ComponentModel.DataAnnotations;

namespace TimeBank.API.Dtos
{
    public record JobScheduleDto
    {
        public int JobScheduleId { get; init; }

        [Required(ErrorMessage = "The day of week is required.")]
        public int DayOfWeek { get; init; }

        [Required(ErrorMessage = "The beginning time is required.")]
        public int TimeBegin { get; init; }

        [Required(ErrorMessage = "The ending time is required.")]
        public int TimeEnd { get; init; }

        public string JobScheduleStatus { get; init; }

        public int JobId { get; init; }
    }
}
