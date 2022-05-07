using System.Threading.Tasks;
using TimeBank.Repository.Models;

namespace TimeBank.API.Hubs.Clients
{
    public interface IMessageClient
    {
        Task ReceiveMessage(Message message);
    }
}
