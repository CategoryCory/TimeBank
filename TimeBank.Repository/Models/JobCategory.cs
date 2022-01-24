using System.Collections.Generic;

namespace TimeBank.Repository.Models
{
    public class JobCategory
    {
        public int JobCategoryId { get; set; }
        public string JobCategoryName { get; set; }
        public string JobCategorySlug { get; set; }

        public ICollection<Job> Jobs { get; set; }
    }
}
