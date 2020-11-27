using System.Threading.Tasks;
using Ctron.API.DTO;

namespace Ctron.API.Authentication
{
    public interface IApiAuthentication
    {
        public Task<VerifiedUser> AuthenticateUser(UserDTO userModel);
        public Task<bool> Register(RegisterModel userModel);
    }
}