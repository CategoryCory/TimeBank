using TimeBank.Repository.Models;

namespace TimeBank.Services.Contracts
{
    public interface IMessageService
    {
        Task<MessageThread> GetMessageThreadByIdAsync(int threadId);
        Task<MessageThread> GetMessageThreadByJobAndParticipantsAsync(int jobId, string toUserId, string fromUserId);
        Task<List<Message>> GetAllMessagesByThreadAsync(int threadId);
        Task<ApplicationResult> CreateMessageThreadAsync(MessageThread messageThread);
        Task<ApplicationResult> AddNewMessageToThreadAsync(Message message, int threadId);
        Task<ApplicationResult> SetMessageToReadAsync(int messageId);
    }
}