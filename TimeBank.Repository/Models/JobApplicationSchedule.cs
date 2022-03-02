namespace TimeBank.Repository.Models
{
    public class JobApplicationSchedule
    {
        public int JobApplicationScheduleId { get; set; }

        public int JobScheduleId { get; set; }
        public JobSchedule JobSchedule { get; set; }

        public int JobApplicationId { get; set; }
        public JobApplication JobApplication { get; set; }
    }
}
