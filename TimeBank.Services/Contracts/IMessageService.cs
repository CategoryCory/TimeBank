using TimeBank.Repository.Models;

namespace TimeBank.Services.Contracts
{
    public interface IMessageService
    {
        Task<List<Message>> GetAllMessagesByThreadAsync(int threadId);
        Task<ApplicationResult> AddNewMessageToThreadAsync(Message message, int threadId);
        Task<ApplicationResult> SetMessageToReadAsync(int messageId);
    }
}