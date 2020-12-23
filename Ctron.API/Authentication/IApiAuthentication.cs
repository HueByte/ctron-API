using System.Threading.Tasks;
using Ctron.API.DTO;
using Ctron.API.Services;
using Ctron.Infrastructure.Models;

namespace Ctron.API.Authentication
{
    public interface IApiAuthentication
    {
        public Task<ApiResponse<VerifiedUser>> AuthenticateUser(UserDTO userModel);
        public Task<ApiResponse<int>> Register(RegisterModel userModel);
    }
}