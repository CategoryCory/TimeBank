using System.Collections.Generic;

namespace TimeBank.Repository.Models
{
    public class JobSchedule
    {
        public int JobScheduleId { get; set; }
        public int DayOfWeek { get; set; }
        public int TimeBegin { get; set; }
        public int TimeEnd { get; set; }

        public int JobId { get; set; }
        public Job Job { get; set; }

        public ICollection<JobApplicationSchedule> JobApplicationSchedules { get; set; }
    }
}
