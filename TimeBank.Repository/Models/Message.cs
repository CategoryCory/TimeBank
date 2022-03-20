namespace TimeBank.Repository.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public bool IsFromSender { get; set; }
        public string Body { get; set; }
        public bool IsRead { get; set; }

        public int MessageThreadId { get; set; }
        public MessageThread MessageThread { get; set; }
    }
}
