using System.Threading.Tasks;
using TimeBank.Repository.IdentityModels;

namespace TimeBank.API.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(ApplicationUser user);
    }
}