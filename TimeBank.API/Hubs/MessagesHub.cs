using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using TimeBank.API.Hubs.Clients;
using TimeBank.Repository.Models;

namespace TimeBank.API.Hubs
{
    public class MessagesHub : Hub<IMessageClient>
    {
        public async Task SendMessage(string userId, Message message)
        {
            // Save message to database

            // Send message to appropriate user
            await Clients.User(userId).ReceiveMessage(message);
        }
    }
}
