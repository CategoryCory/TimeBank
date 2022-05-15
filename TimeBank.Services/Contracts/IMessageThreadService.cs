using TimeBank.Repository.Models;

namespace TimeBank.Services.Contracts
{
    public interface IMessageThreadService
    {
        Task<ApplicationResult> CreateMessageThreadAsync(MessageThread messageThread);
        Task<MessageThread> GetMessageThreadByIdAsync(int threadId);
        Task<MessageThread> GetMessageThreadByJobAndParticipantsAsync(int jobId, string toUserId, string fromUserId);
    }
}