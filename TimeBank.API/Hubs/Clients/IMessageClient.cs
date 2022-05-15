using System.Threading.Tasks;
using TimeBank.API.Dtos;

namespace TimeBank.API.Hubs.Clients
{
    public interface IMessageClient
    {
        Task ReceiveMessage(MessageResponseDto message);
    }
}
