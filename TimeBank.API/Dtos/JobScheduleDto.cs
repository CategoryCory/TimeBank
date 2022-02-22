using System.ComponentModel.DataAnnotations;

namespace TimeBank.API.Dtos
{
    public class JobScheduleDto
    {
        [Required(ErrorMessage = "The day of week is required.")]
        public int DayOfWeek { get; set; }

        [Required(ErrorMessage = "The beginning time is required.")]
        public int TimeBegin { get; set; }

        [Required(ErrorMessage = "The ending time is required.")]
        public int TimeEnd { get; set; }

        public int JobId { get; set; }
    }
}
