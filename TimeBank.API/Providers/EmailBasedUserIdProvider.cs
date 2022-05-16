using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace TimeBank.API.Providers
{
    public class EmailBasedUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ClaimTypes.Email)?.Value!;
        }
    }
}
