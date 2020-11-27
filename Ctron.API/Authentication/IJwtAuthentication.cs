using Ctron.API.DTO;

namespace Ctron.API.Authentication
{
    public interface IJwtAuthentication
    {
        public VerifiedUser GenerateJsonWebToken(VerifiedUser user);
    }
}