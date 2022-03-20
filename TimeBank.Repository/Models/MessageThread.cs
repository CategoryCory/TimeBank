using System;
using System.Collections.Generic;
using TimeBank.Repository.IdentityModels;

namespace TimeBank.Repository.Models
{
    public class MessageThread
    {
        public int MessageThreadId { get; set; }
        public DateTime CreatedOn { get; set; }

        public int JobId { get; set; }
        public Job Job { get; set; }

        public string ToUserId { get; set; }
        public ApplicationUser ToUser { get; set; }

        public string FromUserId { get; set; }
        public ApplicationUser FromUser { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
