using System;
using TimeBank.Repository.IdentityModels;

namespace TimeBank.Repository.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public bool IsFromSender { get; set; }
        public string Body { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadOn { get; set; }

        public int MessageThreadId { get; set; }
        public MessageThread MessageThread { get; set; }

        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
    }
}
