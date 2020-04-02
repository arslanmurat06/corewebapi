using System.Threading.Tasks;
using advancedwebapi.Models;
using advancedwebapi.Services;

namespace advancedwebapi.Auth
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExists(string username);

    }
}